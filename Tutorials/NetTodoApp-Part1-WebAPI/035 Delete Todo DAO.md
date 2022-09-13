# Delete Todo in Data Access Layer

You should already have a compile error in TodoFileDao because of the added method to the ITodoDao interface. Let's fix this, by implementing the method.

We actually did the remove part, when doing the Update method. We could refactor that part out into our new method, and have the Update method first call `RemoveAsync()` and then `CreateAsync()`.

Or you can just copy the remove part of `Update()` to the Delete method. Your pick.

<details>
<summary>hint</summary>

```csharp
public Task DeleteAsync(int id)
{
    Todo? existing = context.Todos.FirstOrDefault(todo => todo.Id == id);
    if (existing == null)
    {
        throw new Exception($"Todo with id {id} does not exist!");
    }

    context.Todos.Remove(existing); 
    context.SaveChanges();
    
    return Task.CompletedTask;
}
```

</details>