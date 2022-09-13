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
Just add a "set;" to the Owner. This is also needed for IsCompleted, and Title.

```csharp
public class Todo
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; }

    public bool IsCompleted { get; set; }

    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}
```

Again, we could put logic like checking that a completed Todo is not un-completed into the Todo class. 
But, _consistency is key_, and as we previously made the decision to put validation logic into the application layer, we will stick with that decision. 

##### Logic update method 

We are now ready for the logic implementation. Not all suggested logic from above will be included in this tutorial.

```csharp
    public async Task UpdateAsync(TodoUpdateDto dto)
    {
        Todo? existing = await todoDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
        
        if (existing.IsCompleted && !dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        Todo updated = new Todo(user, dto.Title)
        {
            Id = existing.Id,
            IsCompleted = dto.IsCompleted
        };
        
        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }

    private void ValidateTodo(Todo dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
```

First, this is a somewhat long method, and it would benefit from being refactored into smaller methods, but that will add some complexity to explaining things.

The method first finds an existing Todo.\
Then the new assignee User is found.\
Then we check that the Todo is not completed and is changed to un-completed.

Then a new Todo is created with the updated data. Alternatively, the existing Todo could be modified, but because it is being referenced from the FileContext class, i.e. still kept in the list, we may encounter unwanted behaviour: If the validation in line 27 does not go through, we would need to undo the modifications.

Finally the new Todo with updated values is passed to the DAO layer.

Let's go and fix that layer next.
