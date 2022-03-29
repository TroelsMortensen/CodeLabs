# Add Todo

Next up, we want to be able to add a new Todo, so we must implement the relevant method in the `TodoHttpClient` class.

The `AddAsync()` method looks like this:

```csharp
public async Task<Todo> AddAsync(Todo todo)
{
    using HttpClient client = new();

    string todoAsJson = JsonSerializer.Serialize(todo);

    StringContent content = new(todoAsJson, Encoding.UTF8, "application/json");

    HttpResponseMessage response = await client.PostAsync("https://localhost:7204/todos", content);
    string responseContent = await response.Content.ReadAsStringAsync();
    
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception($"Error: {response.StatusCode}, {responseContent}");
    }
    
    Todo returned = JsonSerializer.Deserialize<Todo>(responseContent, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    })!;
    
    return returned;
}
```

Again, an HttpClient is created.

The argument-Todo is serialized to json. That is then wrapped into a `StringContent` class, along with the encoding, and the format.

The client is used to make a POST request, to the URI, with the StringContent object. This gets a response back.

We check the status code, and if an error occured, an exception is thrown, to be caught by the UI so a message can be shown to the user.

If no errors happened, the content of the response is deserialized into the finalized Todo object. It now has an ID, in case we need that for anything.

The result Todo is returned.

## Test

Run things again, go and create a new Todo and verify it shows up in the overview.
