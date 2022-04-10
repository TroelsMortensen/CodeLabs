# Deleting a Todo

To delete a Todo, you'll first need to find it by Id, then remove that object. Give it a go. Check the hint afterwards.

<details>
<summary>Hint</summary>

```csharp
public async Task DeleteAsync(int id)
{
    Todo? existing = await context.Todos.FindAsync(id);
    if (existing is null)
    {
        throw new Exception($"Could not find Todo with id {id}. Nothing was deleted");
    }

    context.Todos.Remove(existing);
    await context.SaveChangesAsync();
}
```

The `FindAsync(..)` either returns a Todo with the provided `id` or `null`. Hence the question mark: `Todo?`.\
Alternative, methods `First(..)` or `FirstOrDefault(..)` are good to find a single item by some criteria. They both take a predicate (lambda expression) as argument.\
If no item is matched, the first method will throw an exception. The latter will return `null`.

</details>