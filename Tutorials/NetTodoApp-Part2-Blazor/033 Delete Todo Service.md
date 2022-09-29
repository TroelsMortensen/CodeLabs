# Delete Todo in the Todo Service

We start with the client layer.

## The Interface
First we define the method in the `ITodoService` interface. It does not need to return anything, and the argument is just the id of the Todo, we want to delete.

Put this method in the interface:

```csharp
Task DeleteAsync(int id);
```

## The Implementation
Then we need the implementation of the method, in TodoHttpClient.

We already have a DELETE endpoint in the `TodosController`. Implement the Delete method in TodoHttpClient yourself.

<details>
<summary>hint</summary>

```csharp
public async Task DeleteAsync(int id)
{
    HttpResponseMessage response = await client.DeleteAsync($"Todos/{id}");
    if (!response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();
        throw new Exception(content);
    }
}
```

</details>