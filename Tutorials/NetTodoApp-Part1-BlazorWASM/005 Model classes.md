# Model classes

We are going to need to classes: User and Todo.


### Todo

Inside your Shared project, create a new directory: Models. In here create a new class, call it `Todo`.

The `Todo` class needs properties for the data, a `Todo` should hold:

```csharp
public class Todo
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public bool IsCompleted { get; }
    

    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}
```

We have created a constructor, which only takes two of the four properties as arguments. 
The intention is that the `Id` should be set automatically by whatever class persists the data, 
and you cannot create a `Todo`, which is initially already completed, 
so we just default `IsCompleted` to false, by not setting it.

### User

We also need a User object. Create a new class, "User", inside the "Models" directory. It should look like this:

```csharp
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
```

In many applications the user name is unique, which might make the `Id` property redundant. 
We could, in fact, leave out `Id`, 
but I choose to keep it, because that makes it easier to include a feature, where a user can change their user name. 
It also adds a tiny bit of extra complexity to the system, which might be good as an example. 

### How to connect models

Now, we have a clear connection between Todos and Users. 
The Todo has an `Owner`, which should reference the User to which this Todo is assigned. 

Alternatively, the User class could have a property with the type of a List of Todos, e.g.:

```csharp
    public List<Todo> Todos { get; set; }
```

We could have properties in both directions, but bidirectional associations in the domain model classes can be difficult to maintain. 
Having just one-direction association will, however, potentially make retrieving data a bit more complex. 
So, either approach has drawbacks.

Given that it is a Todo app, the Todo is a key model, and we let the Todo keep track of its assignee, instead of the other way around. 