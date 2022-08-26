# Add Todo in Data Access Layer
We need to fix two things in this layer, the method for finding a user by id and then the method for storing the Todo.

### Find User

You should currently have a compile error in TodoFileDao, because the interface defines a method not yet implemented in the class.\
Let's go ahead and implement that method.

Given an Id we want to return the associated User, or null if none is found. Give it a go yourself first, and then look at the hint below:


<details>
<summary>hint</summary>

```csharp
public Task<User?> GetById(int id)
{
    User? existing = context.Users.FirstOrDefault(u =>
        u.Id == id
    );
    return Task.FromResult(existing);
}
```

</details>


### Store Todo
First, we need a new `TodoFileDao` class, put it in FileData/DAOs.

Then we need the method implemented. It should receive the Todo, set an Id, persist the Todo, and then return it.

Give this a go yourself, and then look at the hint below:

<details>
<summary>hint</summary>

```csharp
public class TodoFileDao : ITodoDao
{
    private readonly FileContext context;

    public TodoFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Todo> Create(Todo todo)
    {
        int id = 1;
        if (context.Todos.Any())
        {
            id = context.Todos.Max(t => t.Id);
            id++;
        }

        todo.Id = id;
        
        context.Todos.Add(todo);
        context.SaveChanges();

        return Task.FromResult(todo);
    }
}
```

</details>

Now we just need the Web API, and then we can test this feature.