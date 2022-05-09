# Authentication manager interface
We need a class, which can handle logging in and logging out.

Initially we will just create the interface. We will provide the implementation later.

Create a new directory, call it Authentication.

Inside the Authentication directory, create the following interface:

```csharp
using System.Security.Claims;

namespace BlazorLoginApp.Authentication;

public interface IAuthManager
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
```

This interface (and its implementation) will be used in the Blazor app, whenever we wish to log in or out.

The property at the bottom is an Action. 
The idea is that another class (`SimpleAuthenticationStateProvider`) will listen to the IAuthManager implementation for changes in authentication state, i.e. an event will be fired whenever someone logs in or out.
The Blazor framework can then react to this.

