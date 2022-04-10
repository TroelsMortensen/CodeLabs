# Get All Todos

Now, almost everything is running and set up. We just need to implement the methods to interact with the TodoContext.

## Get All

We want to test this on the fly, meaning we take the methods in the same order they are needed by the Blazor app. Sort of. That means the `GetAsync()` method comes first.

It looks like this:

```csharp
public async Task<ICollection<Todo>> GetAsync()
{
    ICollection<Todo> todos = await context.Todos.ToListAsync();
    return todos;
}
```

Notice `async` is added to the method signature.

The `ToListAsync()` method will load all Todos and return them as a List. We define the variable as `ICollection` because that is what the method must return.

We are essentially loading all Todos into memory. This may not scale well, but for this toy example, it is just fine.

## Testing

We should now be able to provide a Collection of all Todos, let's test this.

Run the Web API. Run the Blazor app.

Open the view-all-todos page.

Notice how all Todos have an Id, even though we didn't provide one in the seeding method. That's because an int Primary Key is by default a SERIAL, i.e. if you leave the Id as 0 (the default value), the database will automatically select the next available number as Id.