# Update Todo Data Access Layer

In TodoFileDao implement the new methods from the interface, `Update()` and `GetById()`.

The `GetById()` should just take Id and return either a matching Todo or return null. 

The `Update()` must fetch the existing todo, update the properties with the incoming data, and save the changes.\
It is arguable that the process of updating the properties belongs in the Logic layer, because "updating a Todo" contains rules about what exactly is updated. So, in the Logic layer we could retrieve a Todo, overwrite the property values, and have a DAO method called e.g. `Overwrite(Todo todo)`, which would just overwrite the existing Todo in persistence.\
Either way, you have to make a choice about your approach. In this case, we will do the actual update part in the DAO.

Give it a go, then look at the hint below.

<details>
<summary>hint</summary>

```csharp
public Task<Todo?> GetById(int todoId)
{
    Todo? existing = context.Todos.FirstOrDefault(t => t.Id == todoId);
    return Task.FromResult(existing);
}

public async Task Update(Todo todo)
{
    Todo existing = (await GetById(todo.Id))!;
    existing.Title = todo.Title;
    existing.IsCompleted = todo.IsCompleted;
    existing.OwnerId = todo.OwnerId;
    context.SaveChanges();
}
```

In the first method, I search for a Todo given the id. It returns a matching Todo or null, if none is found.

In the second method, I reuse the prior method to find an existing Todo. Notice how GetById returns `Todo?`, i.e. it may be null.\
However, I already checked in the logic layer that the Todo actually exists, so I don't need to check for null after line 9.
Instead I suppress the null-warning from the compiler with the `(...)!`, i.e. exclamation mark. This is not strictly needed, it is just to silence the compiler.

</details>