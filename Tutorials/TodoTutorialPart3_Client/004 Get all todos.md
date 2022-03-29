# Get All Todos

The implementation of the method `GetAsync()` looks like this:

```csharp
public async Task<ICollection<Todo>> GetAsync()
{
    using HttpClient client = new ();
    HttpResponseMessage response = await client.GetAsync("https://localhost:7204/todos");
    string content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception($"Error: {response.StatusCode}, {content}");
    }

    ICollection<Todo> todos = JsonSerializer.Deserialize<ICollection<Todo>>(content)!;
    return todos;
}
```

First, an HttpClient is created.

Then a request is made, asynchronously, to the Web API, using the URI: "https://localhost:7204/todos". Your URI may look different.

The content of the message is read, this will either be an error message, or the collection of Todos as json.

The response contains a status code, which will indicate the success-status of the request. If something went wrong, an exception is thrown with the `content`, which should be an error message.\
Again, it is important to display feedback to the user, in case of errors.

Otherwise, the json is de-serialized into a collection of Todos and returned.

This would be a good place to test things.

