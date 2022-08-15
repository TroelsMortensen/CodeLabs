# UserService implementation

We need a class to handle various User related logic.

In a normal Web API you might have at least three layers:

1) Controllers, to receive incoming requests
2) Services, to contain logic, validation, etc
3) Repositories/Data Access Objects to manage persisting data

We will skip the third layer, as we are just creating a "dummy database". In this case a List with two Users hardcoded.

### The Service

Create a new directory: Services.

#### Interface

In here, create a new interface: `IAuthService`.

It looks like this:

```csharp
public interface IAuthService
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(User user);
}
```

Import the User.

We are not really going to use the `RegisterUser()` method, it's just here as an example.

We use return types of Task, even though in this instance nothing will be asynchronous. But, should we wish to improve
on the example, e.g. by adding a database, this would require asynchronous code, so it's better to be ready.

#### Implementation

Next, the implementation, `AuthService`. Just put this class in the same directory, Services.

It looks like this:

```csharp'
public class AuthService : IAuthService
{

    private readonly IList<User> users = new List<User>
    {
        new User
        {
            Age = 36,
            Email = "trmo@via.dk",
            Domain = "via",
            Name = "Troels Mortensen",
            Password = "onetwo3FOUR",
            Role = "Teacher",
            Username = "trmo",
            SecurityLevel = 4
        },
        new User
        {
            Age = 34,
            Email = "jakob@gmail.com",
            Domain = "gmail",
            Name = "Jakob Rasmussen",
            Password = "password",
            Role = "Student",
            Username = "jknr",
            SecurityLevel = 2
        }
    };

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}
```

At the top, we have an `IList<User>` field. It is instantiated to contain two user objects.

The first method, `ValidateUser`, will take the two relevant arguments.\
The first line of the method will look through the `users` list, and find the first User object, which matches the
criteria specified by the lambda expression, i.e. a User object with the same username as provided with the method
parameter.

If no object is found, an exception is thrown. This can then be handled somewhere else (in our case the Controllers).

Then the passwords are checked. Again, if the wrong password is provided, an exception is thrown.

Finally, if a user is found, and the password is correct, we return that user object.

We do it with

```csharp
return Task.FromResult(existingUser)
```

because the return type is `Task<User>`, but the method is not marked `async`. In that case, we need to take the return
variable and put into a Task manually.

The RegisterUser method takes a User object, checks if the username is not null or empty. More checks could be made,
e.g. regarding length or whatever.\
Then the password is checked, again, maybe you want to have at least 16 characters, and upper and lower case characters,
and symbols and numbers. You can put that logic here.\
You probably also need to check if the username is unique. But all that is not really the purpose of this tutorial, so
it's left out.

The User object is added to the list. Then `Task.CompletedTask` is return, the equivalent of void, when working with
Task return types.

Notice that if you restart the Web API, your newly registered User is lost, because there is no persistence.

### Add service

Finally, we need to add the interface and its implementation as services for the dependency inject framework.

In Program.cs add the following line anywhere above `var app = builder.Build();`:

```csharp
builder.Services.AddScoped<IAuthService, AuthService>();
```