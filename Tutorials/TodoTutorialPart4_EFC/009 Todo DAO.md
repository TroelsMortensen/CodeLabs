# Todo DAO

Now that we have the database access in place, i.e. the TodoContext and a generated database, we need to make it possible for your Todo app to use the database.

We need a new Data Access Object class. Currently, we are using `TodoFileDAO.cs`, from the FileData component.\
We want to create an equivalent class, which just uses the TodoContext.

In the EfcData component, create a new class, 'TodoSqliteDAO'. It should implement the ITodoHome interface, from your Domain component.

The initial class looks like this:

```csharp
public class TodoFileDAO : ITodoHome
{

    private readonly TodoContext context;

    public TodoFileDAO(TodoContext context)
    {
        this.context = context;
    }

    public Task<ICollection<Todo>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Todo> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Todo> AddAsync(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Todo todo)
    {
        throw new NotImplementedException();
    }
}
```
