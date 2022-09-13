# Complete Todo Logic
We will start by defining the input and output of the application layer, i.e. the method in the interface.

Again, we could reuse the Todo, but that would require sending along an entire User object too. That is not necessary. Instead, we make a new `TodoUpdateDto`:

```csharp
public class TodoUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }

    public TodoUpdateDto(int id)
    {
        Id = id;
    }
}
```
The idea for this update, is that we only send what needs to _change_.\
Therefore most of the properties are nullable, i.e. if they have no value, no change will be applied on the server.\
This approach scales better. If the Todo had a large number of properties, but we want to only change one, we would otherwise have to send the complete Todo item, with a lot of redundant data.\
The `Id` is required, so it is in the constructor. The rest are optional, so they will be set using the object initializer. 

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
Currently, the Todo class should have no mutator on the `IsCompleted` property. Add that:

```csharp
public class Todo
{
    public int Id { get; set; }
    public User Owner { get; }
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
Here we go.

```csharp
public async Task UpdateAsync(TodoUpdateDto dto)
{
    Todo? existing = await todoDao.GetByIdAsync(dto.Id);

    if (existing == null)
    {
        throw new Exception($"Todo with ID {dto.Id} not found!");
    }

    User? user = null;
    if (dto.OwnerId != null)
    {
        user = await userDao.GetByIdAsync((int)dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
    }

    if (dto.IsCompleted != null && existing.IsCompleted && !(bool)dto.IsCompleted)
    {
        throw new Exception("Cannot un-complete a completed Todo");
    }

    User userToUse = user ?? existing.Owner;
    string titleToUse = dto.Title ?? existing.Title;
    bool completedToUse = dto.IsCompleted ?? existing.IsCompleted;
    
    Todo updated = new (userToUse, titleToUse)
    {
        IsCompleted = completedToUse,
        Id = existing.Id,
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

The idea is we receive the DTO, and only properties with a value, should be updated.

The method first finds an existing Todo, we cannot update something non-existing.\
Then, if the dto specifies a User, the assignee should be updated, so the new assignee User is found.\
Then we check if the completed status should be updated (i.e. the property is not null), and the Todo is not completed and is changed to un-completed. I.e. we are not allowed to un-complete a completed Todo.

Then three variables are defined. We use the [null coalescing operator "??"](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator) to get the correct value.
It works like this: if the value of the dto is not null, use that value. Otherwise use the value on the right side of ??, i.e. the value from the existing Todo.

Then a new Todo is created with the (potentially) updated data. 
Alternatively, the existing Todo could be modified, 
but because it is being referenced from the FileContext class, 
i.e. still kept in the list, we may encounter unwanted behaviour: 
If the validation in line 27 does not go through, 
we would need to undo the modifications.\
Yes, we are doing something because of the implementation of the DAO layer. We could make the DAO layer return a clone instead. We will stick with this solution for now, it is of little impact

Finally the new Todo with updated values is passed to the DAO layer.


##### Next up
Let's go and fix that layer next.
