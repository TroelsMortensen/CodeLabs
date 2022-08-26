# Retrieve Todos from Data Access Layer
Your compiler should complain about the TodoFileDao class, because we added a new method to the ITodoDao interface.\
So, let's implement the method. It is going to work in the same way as what we did when getting users. Give it a go yourself first.

After your own attempt, or if you're stuck, check out my approach in the hint below. 

The part about searching by user name is slightly complicated, but see if you can figure that out yourself.

<details>
<summary>hint</summary>

```csharp
public Task<IEnumerable<Todo>> Get(SearchTodoParametersDto searchParams)
{
    IEnumerable<Todo> result = context.Todos.AsEnumerable();

    if (!string.IsNullOrEmpty(searchParams.Username))
    {
        User? user = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        
        if (user != null)
        {
            int ownerId = user.Id;
            result = result.Where(t => t.OwnerId == ownerId);
        }
    }

    if (searchParams.UserId != null)
    {
        result = result.Where(t => t.OwnerId == searchParams.UserId);
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

The first one is a bit extra, because a Todo does not know about the user name. So, first we look for a User with that user name. If found, we take the ID and filter the Todos using this ID.

The others should be fairly straight forward.

</details>