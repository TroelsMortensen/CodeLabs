# Add Todo

Next up, we want to be able to add a new Todo, so we implement the TodoSqliteDAO::AddAsync method.

It looks like this:

```csharp
public async Task<Todo> AddAsync(Todo todo)
{
    EntityEntry<Todo> added = await context.AddAsync(todo);
    await context.SaveChangesAsync();
    return added.Entity;
}
```

Notice `async` in method signature.

We use the `AddAsync(..)` method, which returns information about the result, among others: The resulting Todo.\
This is neat, if we wish to return the finished Todo, and in this case the Id is set by the database.

So, after saving the changes, the newly added Todo is returned.

## Testing

Test whether this works.