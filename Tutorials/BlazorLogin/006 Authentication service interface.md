# Authentication service interface
We need a class, which can handle logging in and logging out.

Initially we will just create the interface. We will provide the implementation later.

Inside the Authentication directory, create the following interface:

```csharp
using System.Security.Claims;

namespace BlazorLoginApp.Authentication;

public interface IAuthService
{
    public Task LoginAsync(User user);
    public Task LogoutAsync();
    internal Task<ClaimsPrincipal> GetAuthAsync();

    internal Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
```

This interface (and its implementation) will be used in the Blazor app, whenever we wish to log in or out.

What does `internal` mean? It's only accessible inside the namespace. Similar to package-protected in Java.

The property at the bottom is an Action. The idea is that another class will listen to the IAuthService implementation for changes in authentication state, i.e. an event will be fired whenever someone logs in or out.

