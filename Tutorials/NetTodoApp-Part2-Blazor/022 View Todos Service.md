# Get All Todos

We start with the client layer, and first the interface.

We need to be able to retrieve Todos, and request them with filtering. We already have an endpoint for this.

## Interface

In ITodoService interface add the following method:

```csharp
Task<ICollection<Todo>> GetAsync(
        string? userName, 
        int? userId, 
        bool? completedStatus, 
        string? titleContains
    );
```

You don't really need to split it across multiple lines. I do this for readability when there are many arguments.

## Implementation

Next up, we implement the method in TodoHttpClient.

The method for fetching the data initially looks like below. However, the filter is not yet applied. We do that later:

```csharp
public async Task<ICollection<Todo>> GetAsync(string? userName, int? userId, bool? completedStatus, string? titleContains)
{
    HttpResponseMessage response = await client.GetAsync("/todos");
    string content = await response.Content.ReadAsStringAsync();
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception(content);
    }

    ICollection<Todo> todos = JsonSerializer.Deserialize<ICollection<Todo>>(content, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    })!;
    return todos;
}
```

It takes the four filter criteria, all nullable, in case we don't want to apply a specific filter. We currently use none of them. We will modify this code later, when we apply the filters.

It is the usual about making a GET request, checking the status code, and deserializing the response. You have seen this before.
