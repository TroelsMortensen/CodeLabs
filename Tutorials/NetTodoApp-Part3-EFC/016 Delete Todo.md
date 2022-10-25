# Delete Todo

There is just this one method left in the TodoEfcDao class.

It works similarly to the File version: find existing todo, remove it, save changes.

Give it a go.

<details>
<summary>hint</summary>

```csharp
public async Task DeleteAsync(int id)
{
    Todo? existing = await GetByIdAsync(id);
    if (existing == null)
    {
        throw new Exception($"Todo with id {id} not found");
    }

    context.Todos.Remove(existing);
    await context.SaveChangesAsync();
}
```

We check if the Todo exists. Then remove it from the Todos set. Then we save the changes.

</details>