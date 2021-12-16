# IUserService InMemory implementation

The IUserService is an interface which promises functionality to get users (and, later perhaps, create, update, delete
as well).

We must create an implementation for this service. Initially we will make it as simple as possible, just hardcoding a
few users and put them in a list.

In the Services directory, create a new directory, "Impls".

Inside this directory, create the following class:

```csharp
using BlazorLoginApp.Models;

namespace BlazorLoginApp.Services.Impls;

public class InMemoryUserService : IUserService
{
    public async Task<User?> GetUserAsync(string username)
    {
        User? find = users.Find(user => user.Name.Equals(username));
        return find;
    }

    private List<User> users = new()
    {
        new User("Troels", "Troels1234", "Teacher", 3, 1986),
        new User("Maria", "oneTwo3FOUR", "Student", 2, 2001),
        new User("Anne", "password", "HeadOfDepartment", 5, 1975)        
    };
}
```

Above there is a List of three hard-coded `Users` in a list. 

The `GetUserAsync()` will find the first user object with a matching username. If none is found, `null`is returned.

At some later point, this implementation could be swapped out with a class, which interacts with a database. Or a class which makes a call to a server somewhere.  
But that is out of scope for this tutorial.