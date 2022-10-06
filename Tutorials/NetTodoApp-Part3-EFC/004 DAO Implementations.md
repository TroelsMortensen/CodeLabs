# EFC DAO Implementations

Inside EfcDataAccess create a folder (similar to what we have in FileDate): "DAOs".

We start by creating the two implementations, just so they are in place. Then we implement the methods one by one.

Create the two following classes:

## TodoEfcDao class
Create this class in EfcDataAccess/DAOs.

Implement the interface "ITodoDao", then include the inherited methods. Your class now looks like this:

```csharp
public class TodoEfcDao : ITodoDao
{
    public Task<Todo> CreateAsync(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task<Todo?> GetByIdAsync(int todoId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
```

### UserEfcDao class

Then create this class, implement interface and methods:

```csharp
public class UserEfcDao : IUserDao
{
    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
```

## Register services
Let's now swap out the DAO implementations, so that the server uses our new classes here. Obviously nothing will work, but we can then test along the way.

Open WebAPI/Program.cs

Find these lines:

```csharp{2,5}
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, UserFileDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddScoped<ITodoDao, TodoFileDao>();
builder.Services.AddScoped<ITodoLogic, TodoLogic>();
```

Lines 2 and 5 is where we specify that whenever a class requests a IUserDao or ITodoDao, that class wil actually get a UserFileDao or TodoFileDao, respectively.

Swap out the implementations, so it looks like this:

```csharp{2,5}
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, UserEfcDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddScoped<ITodoDao, TodoEfcDao>();
builder.Services.AddScoped<ITodoLogic, TodoLogic>();
```

Notice the 2nd type parameter is changed from e.g. `UserFileDao` to `UserEfcDao`.

## Test Dependencies
Now, the above code in the WebAPI/Program.cs was the only place, where anything _outside_ the FileData component referenced code _inside_ the FileData component. We have now completely detached this component from the system.

You can verify this, by deleting the FileData component, and you should see that your code still compiles.

So, a minor change to two lines of code, and we have swapped a large chunk of functionality. This is where the Dependency Inversion Principle shines.

![img.png](Resources/ThatsPrettyCool.png)

We can now proceed with implementation of the functionality in EfcDataAccess component, without causing changes to the rest of the code base. 

## Test of the System

We can't test much, since we just broke all our functionality. But you should be able to run the Web API, and interact with the endpoints through the Swagger page. You will just get an error every time.

Before we can start fixing that, we need to set up the Entity Framework Core and the database.

