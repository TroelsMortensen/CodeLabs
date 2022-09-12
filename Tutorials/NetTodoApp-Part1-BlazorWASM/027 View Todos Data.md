# Retrieve Todos from Data Access Layer
Your compiler should complain about the TodoFileDao class, because we added a new method to the ITodoDao interface.\
So, let's implement the method. It is going to work in the same way as what we did when getting users. Give it a go yourself first.

After your own attempt, or if you're stuck, check out my approach in the hint below. 

The part about searching by user name is slightly complicated, but see if you can figure that out yourself.

Remember, all search parameters can be used by themself, applied together with any other, or left out.

<details>
<summary>hint</summary>

```csharp
public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParams)
{
    IEnumerable<Todo> result = context.Todos.AsEnumerable();

    if (!string.IsNullOrEmpty(searchParams.Username))
    {
        // we know username is unique, so just fetch the first
        result = context.Todos.Where(todo =>
            todo.Owner.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
    }

    if (searchParams.UserId != null)
    {
        result = result.Where(t => t.Owner.Id == searchParams.UserId);
    }

    if (searchParams.CompletedStatus != null)
    {
        result = result.Where(t => t.IsCompleted == searchParams.CompletedStatus);
    }

    if (!string.IsNullOrEmpty(searchParams.TitleContains))
    {
        result = result.Where(t =>
            t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
    }

    return Task.FromResult(result);
}
```

Again, we just have one if-statement after the other, one for each search parameter. 

The first case looks for all todos, where their Owner's username is equal to the search parameter, ignoring case.

The others should be fairly straight forward.

</details>