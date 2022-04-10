# Getting All Todos by Criteria

In the previous tutorial it was at the end suggested that you implement an API endpoint, which could take arguments and find a subset of the Todos based on these arguments, i.e. filtering the result.

Let's add that method, the filtering mechanism is interesting.

First the interface, ITodoHome, add a method:

```csharp
public Task<ICollection<Todo>> GetAsync(int? userId, bool? isCompleted);
```

Both arguments are nullable, so they can be left out.

If you have many different arguments for filtering, I suggest you make an object with relevant properties, to avoid a method with 10+ arguments.

It could look like this:

```csharp
public class TodoFilter{
    public int? UserId {get;set;}
    public bool? IsCompleted {get;set;}
```

You will have to implement this method in `EfcData.TodoSqliteDAO`, `FileData.TodoFileDAO`, and `HttpServices.TodoHttpClient`. In the latter two class, you can leave the method with it's default implementation for now, and update later, if needed.

But for `TodoSqliteDAO` we implement the functionality. Give it a go first, yourself. Then look at the hint below.\
Remember, you must be able to apply all, some, or none of the filters.

<details>
<summary>Hint</summary>

```csharp
public async Task<ICollection<Todo>> GetAsync(int? userId, bool? isCompleted)
{
    IQueryable<Todo> todos = context.Todos.AsQueryable();
    if (userId != null)
    {
        todos = todos.Where(todo => todo.OwnerId == userId);
    }

    if (isCompleted != null)
    {
        todos = todos.Where(todo => todo.IsCompleted == isCompleted);
    }

    ICollection<Todo> result = await todos.ToListAsync();
    return result;
}
```

When using LINQ with EFC, methods like `Where()`, do not execute anything right away. Instead we are constructing a query. When the result is needed, the query is executed against the database.

So, in this case, we start by getting an IQueryable. This represents the entire Todo table, but nothing has been loaded yet. It's sort of lazy loading.\
The query is expanded upon in the first if-statement, if relevant. And then in the second if-statement, if relevant, we add further to the query.

Finally, with the call `ToListAsync()`, we _materialize_ the data into memory, as a List.

If this was SQL, we build up the statement step by step:

```sql
SELECT *
FROM Todos
```
Then:
```sql
SELECT *
FROM Todos
WHERE id = {id}
```
And then
```sql
SELECT *
FROM Todos
WHERE   Id = {id}
        AND
        IsCompleted = {isCompleted}
```

#### Challenge
The above method could be rewritten to just a single Where() call, if you're clever with your boolean algebra. It would just look like:

```csharp
return await todos.Where(...).ToListAsync();
```

The details are left to the reader to figure out.

</details>

## Testing

Now, if you want to test this (and don't have it), you'll need a Web API endpoint, which can accept query parameters.