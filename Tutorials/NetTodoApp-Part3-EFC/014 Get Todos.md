# Get Todos

This is the method `TodoEfcDao::GetAsync()`.

It is going to look similar to the `TodoFileDao.GetAsync()`, but similar to the Get User method, we will use an IQueryable instead of IEnumerable.

Give it a go yourself. Remember we want to also load the Users, i.e. `Todo::Owner`, with the `Include()` method.

<details>
<summary>hint</summary>

```csharp
public async Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParams)
{
    IQueryable<Todo> query = context.Todos.Include(todo => todo.Owner).AsQueryable();
    
    if (!string.IsNullOrEmpty(searchParams.Username))
    {
        // we know username is unique, so just fetch the first
        query = query.Where(todo =>
            todo.Owner.UserName.ToLower().Equals(searchParams.Username.ToLower()));
    }
    
    if (searchParams.UserId != null)
    {
        query = query.Where(t => t.Owner.Id == searchParams.UserId);
    }
    
    if (searchParams.CompletedStatus != null)
    {
        query = query.Where(t => t.IsCompleted == searchParams.CompletedStatus);
    }
    
    if (!string.IsNullOrEmpty(searchParams.TitleContains))
    {
        query = query.Where(t =>
            t.Title.ToLower().Contains(searchParams.TitleContains.ToLower()));
    }

    List<Todo> result = await query.ToListAsync();
    return result;
}
```

The method is structured in the same way as the previous version in TodoFileDao.\

Notice, we again use the IQueryable, which _just represents an SQL statement being constructed_.\
We are not executing anything against the database until the second last statement `query.ToListAsync()`.\
This is where the SQL is sent to the database, executed, and a result is returned.

</details>

## Test
Create a few more Todos, maybe through the Database tool. The IsCompleted is represented by 0 or 1, i.e. false or true.

Then use Swagger to test. Try various filter parameters.

You will notice the Owner objects are also loaded in the result:

![img.png](Resources/GetTodosTest.png)

The "owner" field is included here, because of the `Include(todo => todo.Owner)` call at the start of the method.