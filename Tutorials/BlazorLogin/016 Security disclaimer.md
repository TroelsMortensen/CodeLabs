# Security

This login system is a toy system. It is meant to show how to play around with the authorization of a blazor app. However, it is not particularly secure.

After you log in, you can right-click to inspect the page (1), then you can open the Application tab (2), you may need to click (3) to find it.

![img_14.png](img_14.png)

In the Application tab, you can see the user as json, we stored in the session storage:

![img_16.png](img_16.png)

This data can also easily be modified, meaning I could change my Role to be something else, or raise my security level.  
After a page refresh, the blazor authorization would retrieve this updated user instead.

There are some things, we can do to improve this, at the cost of potentially more network calls. So, you have to weigh the options.

### The minor fix
In the `AuthServiceImpl` class, the method `GetAuthAsync()`, here we fetch the stored user from session storage.
```csharp
    public async Task<ClaimsPrincipal> GetAuthAsync()
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

After the user is deserialized, we can use the `userService` to fetch the User from the database again, meaning we update the information of the User object.
```csharp
public async Task<ClaimsPrincipal> GetAuthAsync()
{
    string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
    User? user = null;
    if (!string.IsNullOrEmpty(userAsJson))
    {
        user = JsonSerializer.Deserialize<User>(userAsJson);
        user = await userService.GetUserAsync(user.Name);
    }
    
    ClaimsPrincipal principal = CreateClaimsPrincipal(user);

    return principal;
}
```
This would make tampering with the user info a lot harder.

### Last minor optimization
As mentioned earlier, we could also store the ClaimsPrincipal to improve efficiency, something like this:
```csharp
public async Task<ClaimsPrincipal> GetAuthAsync()
{
    if (principal != null)
    {
        return principal;
    }

    string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
    if (string.IsNullOrEmpty(userAsJson))
    {
        return new ClaimsPrincipal(new ClaimsIdentity());
    }
    
    User? user = JsonSerializer.Deserialize<User>(userAsJson);
    user = await userService.GetUserAsync(user.Name);
    principal = CreateClaimsPrincipal(user);
    return principal;
}
```
This would need a field variable for the principal, and we need to **set it** when logging in and **clear it** when logging out (assign null).