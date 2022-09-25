# Application layer

## SOLID
We are of course applying our SOLID design principles, in this case specifically the ones about separation of concern, i.e. the S and I.\
The result is that we must create a new vertical slice to handle Todo items. We already have a vertical slice for Users, i.e. Controller, Logic, DAO. Now we need the same for Todos.

And again we apply a Domain Drive Design approach.\

1) Logic interface: What is the contract? I.e. the data received and returned.
2) DAO interface: What does the Logic class wish to persist?
3) Logic implementation: What is the business logic involved in creating a Todo item.

## New classes and interfaces
First we actually need the interface, let's call it "ITodoLogic", and put it in Application/LogicInterfaces.

###### Comment:
You will notice we are now organizing our code by layer. All DAO interfaces in the same directory, all logic interfaces in the same directory.

An alternative would be to create a directory for everything related to Users, and another for the classes and interfaces related to Todos. Sometimes this latter approach is preferred, because that kind of organization is more coherent. The important thing is you pick an organization-approach, and that you are consistent.

## The ITodoLogic interfaces
We need to define the contract: what does the logic layer need, what does it give back? I.e. the argument and the return type.

#### Argument
In order to create a Todo we need to receive the following pieces of data:

* Title
* Owner ID

The Todo class contains another two properties not relevant here, so similar to when we created a User, we can either just reuse the Todo class and leave some properties empty, or we can create a specialized DTO.\
We'll go with the latter.

Create the following class inside Domain/DTOs:

```csharp
namespace Domain.DTOs;

public class TodoCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }

    public TodoCreationDto(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}
```

#### Return
Similarly to the feature of creating a new User, some things are done to the Todo item, e.g. setting an ID. We therefore wish to return the finalized Todo item.

#### The interface method
That leaves us the following method in `ITodoLogic`:

```csharp
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> CreateAsync(TodoCreationDto dto);
}
```

Remember that all methods in an interface are implicitly "public", we don't need to put that in front of the method.

## The DAO interface
First, we need to create it. It goes inside Application/DaoInterfaces, call it "ITodoDao".

The method takes a Todo and returns a Todo (because the Id is set).

Like this:

```csharp
Task<Todo> CreateAsync(Todo todo);
```

### The logic
We have the input and output. But what is supposed to happen inside the box? It's similar to what we did with the user, certain rules must be adhered to, when creating Todo:

* Title must be set, we can put a min and max length on it.
* The Todo must be assigned to an existing User
* The Id is set by the Data Access layer
* The completed status of a new Todo is always false, i.e. "not completed"

First, we need a new TodoLogic class:

```csharp
public class TodoLogic : ITodoLogic
{
    private readonly ITodoDao todoDao;
    private readonly IUserDao userDao;

    public TodoLogic(ITodoDao todoDao, IUserDao userDao)
    {
        this.todoDao = todoDao;
        this.userDao = userDao;
    }

    public Task<Todo> CreateAsync(TodoCreationDto todo)
    {
        throw new NotImplementedException();
    }
}
```

Fix import errors.

The class implements the interface, the method is defined, though currently without body. The constructor receives the `ITodoDao`, and also the `IUserDao`.

#### The methods

Then the functionality.
We end up with the following two methods:

```csharp
public async Task<Todo> CreateAsync(TodoCreationDto dto)
{
    User? user = await userDao.GetByIdAsync(dto.OwnerId);
    if (user == null)
    {
        throw new Exception($"User with id {dto.OwnerId} was not found.");
    }

    ValidateTodo(dto);
    Todo todo = new Todo(user, dto.Title);
    Todo created = await todoDao.CreateAsync(todo);
    return created;
}

private void ValidateTodo(TodoCreationDto dto)
{
    if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
    // other validation stuff
}
```

First, an existing user is searched for. We use a method `GetById()`, which does not exist. It is not easy to predict all the functionality needed up front, and it is okay to let the Logic layer drive these functionalities, and then implement them as needed.\
We will fix this method shortly.

If no user is found, an exception is thrown.

Then the data is validated in the second method. I could put more rules in here, but that is less relevant for this tutorial. Again, this data could mostly be put into the Todo constructor, to ensure no invalid Todo is created. But the same discussion applies, as the last time.

A new Todo is instantiated and handed over to the Data Access layer, which does its thing, and returns the finalized object. That object is returned out of the Logic layer.

#### GetById
Let's just fix this compile error. You should be able to use <kbd>alt</kbd> + <kbd>enter</kbd> to quick fix this and create the method in the IUserDao interface.

It looks like this:

```csharp
Task<User?> GetByIdAsync(int id);
```

This now causes a compile error in the implementing class, UserFileDao. So, let's get to that one.