# The Authentication State Provider class
Your Blazor app constantly needs to evaluate what to show to the user based on their credentials. 
As mentioned in the beginning, you can show/hide parts of your UI based on the user being logged in or not, and based on their credentials, i.e. Claims.

The app need to get access to this authentication state, and that happens through a class, which we extend.

Create a new directory in BlazorWasm, call it "Auth".

In here create the class CustomAuthProvider:

```csharp
using System.Security.Claims;
using BlazorWasm.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasm.Auth;

public class CustomAuthProvider: AuthenticationStateProvider
{
    private readonly IAuthService authService;

    public CustomAuthProvider(IAuthService authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();
        
        return new AuthenticationState(principal);
    }
    
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}
```

The Blazor framework knows about the abstract superclass, `AuthenticationStateProvider`. We then provide a subclass, which can give the app the authentication state it requires, in the form of a ClaimsPrincipal. 
So, whenever the app needs to know whether it should show or hide some piece of UI, it will ask our class here about the user credentials, i.e. a ClaimsPrincipal, and then evaluate accordingly.

The field variable is a reference to the IAuthService interface we just created. Notice that we don't know anything about the implementation, or whether JWT is used or whatever. Again, Dependency Inversion Principle. If we wish to change authentication strategy, we can do that isolated.

The constructor receives the instance of IAuthService, i.e. we use dependency injection. And then we subscribe to the `OnAuthStateChanged` event from the IAuthService. I.e. whenever the authentication state changes through logging in or out, this class wants to know about it, and react to it.\
What this class then does is call the method NotifyAuthenticationStateChanged, which is located in the AuthenticationStateProvider, i.e. the superclass. This will cause the app to update.

The method `GetAuthenticationStateAsync()` is called whenever the blazor app needs to know whether to block or show some UI piece.\
Here we just retrieve the ClaimsPrincipal from the IAuthService and return it.

### Register service
We must tell the blazor app to use our implementation. Open Program.cs, and include the line:

`builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();`

Fix imports.


### Add policies
In Program.cs add the following line:

`AuthorizationPolicies.AddPolicies(builder.Services);`

This will add the same policies to your Blazor app as to your Web API. It's convenient to define them in just one place when they need to be the same.