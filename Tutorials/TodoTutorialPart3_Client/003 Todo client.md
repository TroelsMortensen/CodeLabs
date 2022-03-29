# Todo Client

Create a new class, `TodoHttpClient`.

Make it implement the ITodoHome interface from Domain. Auto-implement the methods.

Your class should now look like:

```csharp
public class TodoHttpClient : ITodoHome
{
    public Task<ICollection<Todo>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Todo> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Todo> AddAsync(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Todo todo)
    {
        throw new NotImplementedException();
    }
}
```

Your interface may look different, if you have implemented a method, which can get a filtered list of Todos. E.g. filtering by completed status, or user-id.

We will implement the methods in the order of their usage in the app, sort of. That means starting with the `GetAsync()` method. 


## Register as service

Currently, your Blazor app is using the `TodoFileDAO` class. That's no longer feasible, as we instead wish to get data from some "remote" Web API (which is just running locally. But it's a separate process).

Open the Program.cs file of Blazor component.

Modify the line where you register a scoped ITodoHome:
```csharp
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<ITodoHome, TodoFileDAO>();
```

The above two lines must instead be just:

```csharp
builder.Services.AddScoped<ITodoHome, TodoHttpClient>();
```