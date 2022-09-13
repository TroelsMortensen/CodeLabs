# Update Todo Data Access Layer

In TodoFileDao implement the new methods from the interface, `UpdateAsync()` and `GetByIdAsync()`.

## Get by ID

The `GetByIdAsync()` should just take Id and return either a matching Todo or return null. Give this a go yourself first, then look at the hint.

<details>
<summary> hint</summary>

```csharp
public Task<Todo?> GetByIdAsync(int todoId)
{
    Todo? existing = context.Todos.FirstOrDefault(t => t.Id == todoId);
    return Task.FromResult(existing);
}
```

You have seen this before, when finding a User.

</details>

## Update

The `UpdateAsync()` must update the existing Todo. To do this, we will just do a _remove_ followed by an _add_. If nothing could be removed, then the Todo does not exist, and an exception should be thrown. 
We cannot update something, which do not exist.

Remember to save.


Give it a go, then look at the hint below.

<details>
<summary>hint</summary>

```csharp
public Task UpdateAsync(Todo toUpdate)
{
    Todo? existing = context.Todos.FirstOrDefault(todo => todo.Id == toUpdate.Id);
    if (existing == null)
    {
        throw new Exception($"Todo with id {toUpdate.Id} does not exist!");
    }

    context.Todos.Remove(existing);
    context.Todos.Add(toUpdate);
    
    context.SaveChanges();
    
    return Task.CompletedTask;
}
```

First the existing todo is found by its ID. If none exist, an exception is thrown. I know, we also checked for the existing Todo in the logic layer, but this method may potentially be reused somewhere else.
This is just an extra safety measure.

We remove the existing Todo from the collection. Then add the new Todo. Essentially we overwrite the existing.

The changes are saved, i.e. written to the file.

Task.Completed task is returned, because the return type is Task, and the method is not marked "async";

</details>