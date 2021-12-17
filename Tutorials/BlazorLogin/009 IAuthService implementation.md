# IAuthService implementation
Now that we have the IUserService, we can create a class that uses it.

In the directory Authentication, create the following class. Explanation below.

```csharp
using System.Security.Claims;
using System.Text.Json;
using BlazorLoginApp.Models;
using BlazorLoginApp.Services;
using Microsoft.JSInterop;

namespace BlazorLoginApp.Authentication;

public class AuthServiceImpl : IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;

    public AuthServiceImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string username, string password)
    {
        User? user = await userService.GetUserAsync(username);

        ValidateLoginCredentials(password, user);

        await CacheUserAsync(user);

        ClaimsPrincipal principal = CreateClaimsPrincipal(user);

        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync();
        ClaimsPrincipal principal = CreateClaimsPrincipal(null);
        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task<ClaimsPrincipal> IAuthService.GetAuthAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        User? user = null;
        if (!string.IsNullOrEmpty(userAsJson))
        {
            user = JsonSerializer.Deserialize<User>(userAsJson);
        }
        ClaimsPrincipal principal = CreateClaimsPrincipal(user);

        return principal;
    }

    private void ValidateLoginCredentials(string password, User? user)
    {
        if (user == null)
        {
            throw new Exception("Username not found");
        }

        if (!password.Equals(user.Password))
        {
            throw new Exception("Password incorrect");
        }
    }

    private async Task CacheUserAsync(User? user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private ClaimsPrincipal CreateClaimsPrincipal(User? user)
    {
        ClaimsIdentity identity = new();
        if (user != null)
        {
            identity = ConvertUserToClaimsIdentity(user);
        }

        ClaimsPrincipal principal = new(identity);

        return principal;
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Role", user.Role),
            new Claim("SecurityLevel", user.SecurityLevel.ToString()),
            new Claim("BirthYear", user.BirthYear.ToString())
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
}
```

Yes, it's a bit of a long one. Luckily for you, you shouldn't need to change too much here to adapt the class to your own Blazor app later.

Of course, if instead of `User` you want another type, like Profile or Account, then you would have to swap that out.

We will take chunks out of the above class, and explain small parts at a time.

But first, two classes:

#### *ClaimsIdentity and ClaimsPrincipal*
The `ClaimsPrincipal` is a class the blazor authentication framework knows about. This is essentially its own user type. You have created a `User`, or Account or Profile or whatever. Blazor uses a `ClaimsPrincipal`.

Your `User` contains information about the user. The `ClaimsPrincipal` also contains information. 
This information is put into a `ClaimsIdentity`, which is essentially just a map of key-value pairs. E.g.:

["Username", "Troels"]  
["Role", "Teacher]  
["SecurityLevel", "3"]  

The Blazor app can then retrieve the relevant information when needed, 
e.g. when checking if the user is allowed to view or interact with certain parts of the app.
Or if a greeting, **"Welcome Anon"**, should be displayed when you are logged in

Okay, the code then:

### Fields and constructor
First, the fields:

```csharp
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;

    public AuthServiceImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }
```
We have the Action, so we can notify the `SimpleAuthenticationStateProvider` about logging in and out. In .NET6 they have introduced a feature, so that you should explicitly define which variables can be null. That is why it is instantiated to `null!`.  
Then the IUserService, which is injected through the constructor. Dependency injection again here. I plan on just keeping users in a `List` for this tutorial, but later the implementation could be swapped out. So to ease the swapping, we depend on an interface, which is provided through the constructor. No dependencies to an implementation.  
The `IJSRuntime` is a class which can call javascript methods. We need that to store some data in the browser.

The constructor receives relevant arguments.

### Log in
```csharp
public async Task LoginAsync(string username, string password)
{
    User? user = await userService.GetUserAsync(username);

    ValidateLoginCredentials(password, user);

    await CacheUserAsync(user);

    ClaimsPrincipal principal = CreateClaimsPrincipal(user);

    OnAuthStateChanged?.Invoke(principal);
}
```

This method is to be called from some page, when we wish to log in. The method is asynchronous, because it does things, which takes time, potentially, like network calls.

`Username` and `password` are provided as arguments.

First, we ask the `userService` to retrieve a `User` object based on the username. Notice I don't pass the password here. This `GetUserAsync` will eventually contact a server somewhere, and we don't want to send the password around for hackers to sniff out.

We get a `User?` back, here the '?' indicates the `user` may be null, this happens if no `User` matches the username.

Then the returned User object is validated. The method is shown later, but it just checks if the user is not null and that the provided password matches the password of the user. If either fails, an exception is thrown.

If all is good, we then cache the user. This means we take the user object, and store it in the browser. Why is this necessary? There are alternatives, but this approach seems to work well. 
The IAuthService will be added as scoped, i.e. a new instance is created whenever a new tab is opened, or the current is refreshed. Experience has shown that refreshes happens occasionally, which results in a new IAuthService instance, meaning you loose data about the currently logged in user: You will have to log in to the app often, which is annoying.  

We create a new ClaimsPrincipal based on the user. Blazor authentication framework works with ClaimsPrincipals. It's just a class to hold information about the user.

Finally, we invoke the `OnAuthStateChanged` to let anyone listening know about the logging in. The `SimpleAuthenticationStateProvider` has subscribed to this action, so it is notified when the action is invoked.

### Log out
This method is to be called when the user wishes to log out.

```csharp
public async Task LogoutAsync()
{
    await ClearUserFromCacheAsync();
    ClaimsPrincipal principal = CreateClaimsPrincipal(null);
    OnAuthStateChanged?.Invoke(principal);
}
```

First, we clear the cached user from the browser storage.  
We then create a `ClaimsPrincipal` from 'nothing', i.e. null. This will clear user information, and essentially say to the authentication framework, that no user is logged in.  
Then we notify the `SimpleAuthenticationStateProvider` that the user has logged out.

### Get authentication
This method is used by `SimpleAuthenticationStateProvider` whenever the user is accessing a page with any kind of authentication/authorization requirement. `SimpleAuthenticationStateProvider` will call this method to retrieve information about the logged in user.

```csharp
public async Task<ClaimsPrincipal> IAuthService.GetAuthAsync()
{
    string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
    User? user = null;
    if (!string.IsNullOrEmpty(userAsJson))
    {
        user = JsonSerializer.Deserialize<User>(userAsJson);
    }
    ClaimsPrincipal principal = CreateClaimsPrincipal(user);

    return principal;
}
```

The method retrieves the data from the browser storage, here we need the javascript. The session storage is sort of like a tiny temporary database, or map, in the browser, where we can put data. The data will be erased when the browser tab is closed, i.e. the session is over.

The user in the session storage is stored as json, so the next line deserializes the json into a user object. 
Again, with '?' to indicate we may have no user stored, which result in  a `null-user`. If anything is retrieved from session storage, we attempt to deserialize it into a user.

We create a `ClaimsPrincipal`, i.e. take the user information and put it into a `ClaimsPrincipal`. Then the `ClaimsPrincipal` is returned.

###### *Optimization* 

This method could be optimized if the reader is interested. Currently, whenever the authentication state is needed (which is potentially quite often), we retrieve the cached user from the browser session storage, and converts it to a ClaimsPrincipal.

This `principal` could be stored in a field variable, to we have a local cache as well. The `GetAuthAsync()` method would then first check if the field is null, if not just return that. This will save us the effort of retrieving the session storage user and converting it.

We should then remember to also clear the local cached upon logging out, i.e. set it to an "empty" ClaimsPrincipal.

Implementation is left to the reader.

### Validation
```csharp
private void ValidateLoginCredentials(string password, User? user)
{
    if (user == null)
    {
        throw new Exception("Username not found");
    }

    if (!password.Equals(user.Password))
    {
        throw new Exception("Password incorrect");
    }
}
```

This method just validates if a user was found, i.e. it is not `null`. And then check if the password stored in the database matches the password typed in by the user.

If encryption of passwords were used, we would first incrypt the `password` argument before comparing to the encrypted password stored in the database, i.e. the data in `user.Password`.

### ClaimsPrincipal
```csharp
private ClaimsPrincipal CreateClaimsPrincipal(User? user)
{
    ClaimsIdentity identity = new();
    if (user != null)
    {
        identity = ConvertUserToClaimsIdentity(user);
    }

    ClaimsPrincipal principal = new(identity);

    return principal;
}
```

This method takes the user, and if it is not `null`, puts the information into a `ClaimsIdentity` object, which is inserted into a `ClaimsPrincipal` and returned.

The ClaimsIdentity is essentially just a map of keys and values. 

### Caching the user

```csharp
private async Task CacheUserAsync(User? user)
{
    string serialisedData = JsonSerializer.Serialize(user);
    await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
}
```

This method takes a user object, serializes it to json, and then stores in the session storage, so we can retrieve it later.

### Clearing the cached user
Whenever we log out, we want to delete the user information stored in the session storage. The below method clears that data:

```csharp
private async Task ClearUserFromCacheAsync()
{
    await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
}
```

### Converting user to claims
And finally, the method which takes your custom `User` object and converts the information into something the Blazor authentication/authorization can understand: A `ClaimsIdentity`.

```csharp
private ClaimsIdentity ConvertUserToClaimsIdentity(User user)
{
    List<Claim> claims = new()
    {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim("Role", user.Role),
        new Claim("SecurityLevel", user.SecurityLevel.ToString()),
        new Claim("BirthYear", user.BirthYear.ToString())
    };

    return new ClaimsIdentity(claims, "apiauth_type");
}
```
We first create a list of claims: key-value pairs.

There are some pre-defined `ClaimsTypes`, e.g. `Name`. If we set this, we have easy access to the username throughout the app.

You may find others of relevans, like `Role`, `Surname`, etc.

But mostly, you can just define your own keys, like I have done above: `"Role"` and `"SecurityLevel"`.

In the end, the ClaimsIdentity is returned.

