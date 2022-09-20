# Service for Handling Todos

We start by defining the interfaces. Then the implementation

## Interfaces

We need to be able to retrieve users, so that we can select an assignee. That already exists in the IUserService.

We also need a new interface, ITodoService. Create this in HttpClients/ClientInterfaces.

It needs a method to create Todos. It does not need to return anything. And the argument is the `TodoCreationDto` we already have.

The interface then looks like this:

```csharp
public interface ITodoService
{
    Task CreateAsync(TodoCreationDto dto);
}
```

## Implementation
Next up, create a new class, TodoHttpClient in side HttpClients/Implementations.

It initially looks like this:

```csharp
public class TodoHttpClient : ITodoService
{
    private readonly HttpClient client;

    public TodoHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public Task CreateAsync(TodoCreationDto dto)
    {
        throw new NotImplementedException();
    }
}
```

Then the implementation. Give it a go yourself first, it is very similar to how we handled creating users.

<details>
<summary>hint</summary>

```csharp
public async Task CreateAsync(TodoCreationDto dto)
{
    HttpResponseMessage response = await client.PostAsJsonAsync("/todos",dto);
    if (!response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();
        throw new Exception(content);
    }
}
```

The argument is serialized. Then the client is used to make a POST request with the JSON. The response is checked for failure, in which case an exception is thrown.

</details>

## Add as Service

Add your new interface and implementation as services in BlazorWASM/Program.cs:

```csharp
builder.Services.AddScoped<ITodoService, TodoHttpClient>();
```