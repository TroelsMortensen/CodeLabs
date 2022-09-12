# Complete Todo Logic
We will start by defining the input and output of the application layer, i.e. the method in the interface.

Again, we could reuse the Todo, but that would require sending along an entire User object too. That is not necessary. Instead, we make a new `TodoUpdateDto`:

```csharp
public class TodoUpdateDto
{
    public int Id { get; }
    public int OwnerId { get; }
    public string Title { get; }
    public bool IsCompleted { get; }

    public TodoUpdateDto(int id, int ownerId, string title, bool isCompleted)
    {
        Id = id;
        OwnerId = ownerId;
        Title = title;
        IsCompleted = isCompleted;
    }
}
```

Nothing needs to be returned. If the update is a success, the client will already have the data. If we had some property, which the server would re-calculate based on the updated data, we might want to return the resulting Todo to the client. That is, however, not our case this time.

First the Logic interface, then the DAO interface, and then the Logic implementation

## Logic Interface
It receives a `TodoUpdateDto` and it returns nothing.    

In case of errors, we throw exceptions.

The method then looks like:
```csharp
Task UpdateAsync(TodoUpdateDto todo);
```

Put that method in the `ITodoLogic` interface.

## DAO Interface
We are going to need two methods here.

#### Update

The DAO interface needs a Todo objet, and there is no need to return anything. This means the Logic implementation will convert from TodoUpdateDto to Todo.

```csharp
Task UpdateAsync(Todo todo);
```
#### Get By Id
The other method is to retrieve a single Todo given an Id. 
We have a `GetAsync()` method in the DAO interface, however, it returns a collection. 
We could probably use this method, but I prefer specialized method to get one Todo.

```csharp
Task<Todo> GetByIdAsync(int id);
```

## Logic Implementation

In the TodoLogic class, implement the new method from the interface.

What needs to be done?

1) We need to verify that there is an existing Todo with the same ID as the one provided. When creating a new User, we looked for an existing User. This functionality could also have been placed in the DAO implementation. But I consider this check as part of the logic of creating/updating, and so I believe it belongs in the Logic layer. The drawback is that we do two interactions with the database, where we might only strictly need one, if we moved the check. In this case the drawback is accepted. 
2) We need to verify the owner ID corresponds to an existing user.
3) We need to validate the new Todo data. Luckily we have that logic already, from when we created a Todo. However, we will have to update the method to take a `Todo` instead of `TodoCreationDto`.
4) We could have more rules, e.g. a User may have a maximum of 5 Todos assigned to them.
5) If the Users had Roles, and the Todos had categories, we could have rules about what kind of category Todos can be assigned to which Roles.
6) Maybe it is not allowed to un-complete a completed Todo, and users should instead create new.



This leads us to the method implementation. Give it a go yourself:
* Check that there is an existing Todo to update
* Validate the data, but modifying the existing `ValidateTodo` to take another type of argument.
* Hand over the Todo to the Data Access layer, through the ITodoDao interface.

This tutorial will ignore most of the business rules. That kind of logic is not in focus here. 
But the reader is encouraged to implement various rules themself.

We need a few things, so I will take you through it.

##### Todo
Currently, the Todo class should have no mutator on the Owner property. We will need that.
Just add a "set;" to the Owner. We will do something similar to the `IsCompleted`, however let's make it so Todos cannot be un-completed once completed:

--------- rework!!!
```csharp
public class Todo
{
    private bool isCompleted;
    
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; }

    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            // cannot un-complete a completed Todo
            if (value == false && isCompleted == true)
            {
                throw new Exception("Cannot un-complete a Todo");
            }

            isCompleted = value;
        }
    }

    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}
```

Notice the explicit private field.

##### Logic update method 


```csharp
public async Task UpdateAsync(Todo todo)
{
    Todo? existing = await todoDao.GetById(todo.Id);

    if (existing == null)
    {
        throw new Exception($"Todo with ID {todo.Id} not found!");
    }

    User? user = await userDao.GetById(todo.OwnerId);
    if (user == null)
    {
        throw new Exception($"User with id {todo.OwnerId} was not found.");
    }

    ValidateTodo(todo);

    await todoDao.Update(todo);
}

private void ValidateTodo(Todo todo)
{
    if (string.IsNullOrEmpty(todo.Title)) throw new Exception("Title cannot be empty.");
    // other validation stuff
}
```

