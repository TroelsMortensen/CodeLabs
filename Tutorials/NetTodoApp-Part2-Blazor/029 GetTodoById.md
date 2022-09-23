# Get Todo By Id

We need to be able to retrieve a single Todo, given it's ID. We could reuse the method, which returns a collection, but I prefer to have a specific method for this.


## The Interface

In ITodoService add the following method signature:

```csharp
Task<TodoBasicDto> GetByIdAsync(int id);
```

## The Implementation
You must make a GET request, with the id, to the Web API. It should return a single `TodoBasicDto`, which is deserialized and returned. Check error status codes.

Give it a try yourself.

<details>
<summary>hint</summary>

```csharp
public async Task<TodoBasicDto> GetByIdAsync(int id)
{
    HttpResponseMessage response = await client.GetAsync($"/todos/{id}");
    string content = await response.Content.ReadAsStringAsync();
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception(content);
    }

    TodoBasicDto todo = JsonSerializer.Deserialize<TodoBasicDto>(content, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    })!;
    return todo;
}
```

This should be pretty standard, no big surprises here. Notice the null-suppressor "!" at the end of line 13. 

</details>


#### Comment
Now the Web API returns a `TodoBasicDto` instead of a Todo. That might be annoying, and we could consider changing it. But let is stick with it for now, to minimize the required changes to existing code.

