# Update Todo Logic
We will start by defining the input and output of the application layer, i.e. the method in the interface.

This time we need a whole Todo item, so we can just use the domain object `Todo`, instead of creating a new DTO.

First the Logic interface, then the DAO interface, and then the Logic implementation

## Logic Interface
It receives a `Todo` and it returns nothing. We could return the updated resulting Todo, but in case of a success response, the client already has all the data.   

In case of errors, we throw exceptions.

The method then looks like:
```csharp
Task Update(Todo todo);
```

## DAO Interface
We are going to need two methods here.

#### Update

The method in the DAO interface is the same. We need the same information, and there is no need to return anything.

```csharp
Task Update(Todo todo);
```
#### Get By Id
The other method is to retrieve a single Todo given an Id. We have a `Get()` method in the DAO interface, however, it returns a collection. We could probably use this method, but I specialized method to get one Todo.

```csharp
Task<Todo> GetById(int id);
```

## Logic Implementation

In the TodoLogic class, implement the new method from the interface.

What needs to be done?

1) We need to verify that there is an existing Todo with the same ID as the one provided. When creating a new User, we looked for an existing User. This functionality could also have been placed in the DAO implementation. But I consider this check as part of the logic of creating/updating, and so I believe it belongs in the Logic layer. The drawback is that we do two interactions with the database, where we might only strictly need one, if we moved the check. In this case the drawback is accepted. 
2) We need to verify the owner ID corresponds to an existing user.
3) We need to validate the new Todo data. Luckily we have that logic already, from when we created a Todo. However, we will have to update the method to take a `Todo` instead of `TodoCreationDto`.  



This leads us to the method implementation. Give it a go yourself:
* Check that there is an existing Todo to update
* Validate the data, but modifying the existing `ValidateTodo` to take another type of argument.
* Hand over the Todo to the Data Access layer, through the ITodoDao interface.

<details>
<summary>hint</summary>

```csharp
public async Task Update(Todo todo)
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

</details>