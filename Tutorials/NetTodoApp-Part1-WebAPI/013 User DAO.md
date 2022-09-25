# User Data Access
With the logic and network layers in place, we just need to be able to store the new User.

Inside FileData component, create a new directory, "DAOs".

Inside this directory, create the class "UserFileDao". The class is seen [here](https://github.com/TroelsMortensen/WasmTodo/blob/002_AddUser/FileData/DAOs/UserFileDAO.cs).

This is the initial setup of the class:

```csharp
public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        throw new NotImplementedException();
    }
}
```

We implemented the `IUserDao` interface from the Application component.

We receive an instance of FileContext through constructor dependency injection.

We have the two methods from the interface, currently without a working body. So, let's get on that.

### Create User
This method should take the User object, assign a unique ID, add it to the collection in the FileContext, and save the changes, so that the data is persisted to the file.

It looks like this:

```csharp
public Task<User> CreateAsync(User user)
{
    int userId = 1;
    if (context.Users.Any())
    {
        userId = context.Users.Max(u => u.Id);
        userId++;
    }

    user.Id = userId;

    context.Users.Add(user);
    context.SaveChanges();

    return Task.FromResult(user);
}
```
If there currently are no Users in the storage, then we just set the Id of the new User to be 1.\
Otherwise:
The `Max()` method looks through all the User objects and returns the max value found from the property `Id`. The result is incremented, and so we know this int is not currently in use as an ID.

The return statement is a bit iffy, because the method signature says to return a Task<User>, but we are not doing anything asynchronous.\
Remember, the Task<User> return type is because later on, when we add a real database, these methods will have to do asynchronous work against the database.

But for now, it is synchronous code, looking like asynchronous. The consequence is just that we have to manually wrap the return value in a Task.

### Get User
Next up, the method to find a user by user name.

You could try and implement that method yourself first, and then afterwards look at the below hint, i.e. my approach:


<details>
<summary>hint</summary>

```csharp
public Task<User?> GetByUsernameAsync(string userName)
{
    User? existing = context.Users.FirstOrDefault(u =>
        u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
    );
    return Task.FromResult(existing);
}
```

The `FirstOrDefault()` method will find the first object matching the criteria specified in the lambda expression.\
If nothing is found, `null` is returned.

In the Equals method I specify that the matching should not consider upper/lower case. I don't want a user called `Troels` and another `troels`.
</details>

