# Entity classes

We are going to need to classes: User and Todo.


### Todo

Inside your Entities project, create a new class, call it Todo.

The `Todo` class needs properties for the data, a `Todo` should hold:

```csharp
public class Todo
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}
```

We have created a constructor, which only takes two of the four properties as arguments. 
The intention is that the `Id` should be set automatically by whatever class persists the data, 
and you cannot create a `Todo`, which is initially already completed, 
so we just default `IsCompleted` to false, by not setting it.

### User

We also need a User object. Create a new class, "User", it should look like this:

```csharp
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
```

In many applications the user name is unique, which would make the `Id` property redundant. We could, in fact, leave out `Id`, 
but I choose to keep it, because that makes it easier to include feature, where a user can change their user name. It also adds a tiny bit of extra complexity to the system, which might be good as an example. 

### How to connect entities

Now, we have a clear connection between Todos and Users. The Todo has an `OwnerId`, which should reference the `Id` of a user. There is, however, no association between the two classes. The `Todo` does not have a property of type `User`. 

This approach is more similar to how we structure tables in a database. We could take (and will in part 3) a more Object Oriented approach, and include a 

```csharp
    public User User { get; set; }
```

property in the Todo class. Alternatively, the User class could have a property with the type of a List of Todos, e.g.:

```csharp
    public List<Todo> Todos { get; set; }
```

Having the associations makes the json-storage more complex, especially when the number of entity objects grows. 
So, for now we model the entities without associations between them. It will, however, potentially make retrieve data a bit more complex. So, either approach has drawbacks.

We will have to modify the entity classes a bit, when we get to part 3 about using Entity Framework Core, and throw away the file storage.