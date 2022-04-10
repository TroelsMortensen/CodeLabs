# Update Todo

Implement the UpdateAsync method.

If you need help, see the hint below. But give it a go first.

<details>
<summary>Hint</summary>

```csharp
public Task UpdateAsync(Todo todo)
{
    context.Todos.Update(todo);
    return context.SaveChangesAsync();
}
```

The Update method will find an existing Todo based on the Id of the argument.

Notice I haven't added `async` to the method signature. The method `SaveChangesAsync` returns a Task, so I just return that.\
Alternatively, I could make the method `async` and await the call to `SaveChangesAsync`, and return nothing.

</details>

Test that it works. Either by editing the Todo item or changing the completed status through the Blazor app.