# Complete Todo

This was actually "update todo". I.e. we could update more properties than just the IsCompleted. That part was handled by the Logic layer.

We need two methods in TodoEfcDao: UpdateAsync and GetByIdAsync.



## GetByIdAsync()

This should be straight forward. Remember to load the Owner as well.

<details>
<summary>hint</summary>

```csharp
public async Task<Todo?> GetByIdAsync(int todoId)
{
    Todo? found = await context.Todos
        .Include(todo => todo.Owner)
        .SingleOrDefaultAsync(todo => todo.Id == todoId);
    return found;
}
```

</details>

## UpdateAsync()

In the FileContext, we would remove, then add a Todo.\
The DbSet has an Update method, which will search for an existing object with the same Id, and just overwrite the data.

Here's the method:

```csharp
public async Task UpdateAsync(Todo todo)
{
    context.Todos.Update(todo);
    await context.SaveChangesAsync();
}
```

#### What is the ChangeTracker?

We do have a potential problem though, which was found later, and happens in specific circumstances.

It's because of the ChangeTracker.

Your DbContext subclass has a kind of _cache_, i.e. the ChangeTracker. 
It keeps previously loaded objects in memory. It keeps added objects, or tracks that objects are removed or updated. 
When you load something from the database, those objects are kept in the ChangeTracker as well. It tracks changes.\
When the SaveChanges is called, everything in the ChangeTracker is submitted to the database in a transaction.\


E.g. if you fetch an object from the database twice in a row, the first time it will be retrieved from the database. It is then held in memory, in the ChangeTracker.\
But the second time, the object already exists in memory, so that is returned instead of contacting the database.

Now, when we update a Todo, we might have a problem:\
The Update() method will load a Todo, put it in the ChangeTracker. The Todo has an Owner, i.e. a User, so that is loaded. The User currently has a List of Todos, so that is loaded. But the List will contain the first loaded Todo, and we get a conflict. We have attempted to load the same Todo into the ChangeTracker twice.

This bug was discovered some time after the guide was created, and a good, correct fix is somewhat outside the scope, currently.\
It involved the Unit of Work pattern, and not using the Update method.

The simple solution would be to just delete the Collection of Todos on the User object:

I.e. delete the highlighted line.

```csharp{5}
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public ICollection<Todo> Todos { get; set; }
}
```

This collection is not currently used anywhere, so it shouldn't be a problem to delete it.\
Now we don't load the circular Todo -> User -> Todo.

You won't have to update the database.

## Test
.. this by running the Web API, and using the PATCH /Todos endpoint.

See if you can complete a Todo. The "try it out" option in Swagger requires you to fill in all data, so find that through another endpoint or the Database tool in Rider.\
Verify result either through Database tool, or another endpoint.

See if you can update a Title.

See if you can re-assign a Todo to a different User.