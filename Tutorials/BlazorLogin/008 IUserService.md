# IUserService

We need an interface which can provide us with user objects. We will put this in a different directory, because it is not directly associated with authentication.  
This isn't really all that relevant, you can put things wherever you wish. But still.

Create a directory, Services.

Inside here, create an interface IUserService:

```csharp
using BlazorLoginApp.Models;

namespace BlazorLoginApp.Services;

public interface IUserService
{
    public Task<User> GetUserAsync(string username);
}
```

Later, we could add other methods for adding, removing, getting all users, updating a user, etc. That is, however, not relevant in this tutorial.

#### Note
This is a "service" class, which later in the course will be moved to the server. The AuthManager will remain on the client side, as that is tightly coupled with the auth framework.