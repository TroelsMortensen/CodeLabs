# Data Access Object

We currently only have one type of domain object, so we just need a single DAO class to provide CRUD operations on Todos.

The finished class is found [here](https://github.com/TroelsMortensen/BlazorTodoApp/blob/Part1/FileData/DataAccess/TodoFileDAO.cs).

This DAO should be located in the FileData component, either put it in the DataAccess directory or create a new.

![img_11.png](Resources/img_11.png)

This class, `TodoFileDAO`, should implement the `ITodoHome` interface from the Domain component, and provide implementations for the
methods.

First, we need access to a `FileContext` instance. We will just receive and assign this in the constructor:

```csharp
public class TodoFileDAO : ITodoHome
{
    private FileContext fileContext;

    public TodoFileDAO(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }
    
    // ...
```

We use constructor dependency injection for the `FileContext`, so we don't have to manually create a new instance. This is
generally a good approach.\
Remember SDJ2 and MVVM:
Controllers, VMs, and Models got what they needed through constructors. We will get the framework to handle this dependency
injection for us.

### Note about `async` and `Task` return types
The interface, `ITodoHome`, has defined a couple of methods, all of which has return type `Task` or `Task<T>`. This is because in a later tutorial, we will add a database instead of file storage.
And when working with the database, we can do many of the interactions asynchronously.

For this File storage, i.e. FileContext of FileDAO, however, there isn't much we can do, or is worth doing, asynchronously. We must still return `Task`, but it is not necessary to make the methods `async`, given that they do not perform anything asynchronous.
Instead, we must wrap the returned value in a Task. That is what is happening in the return statements of the methods described below.


### Getting all todos
We'll start with the `GetAsync()` method:
```csharp
public Task<ICollection<Todo>> GetAsync()
{
    ICollection<Todo> todos = fileContext.Todos;
    return Task.FromResult(todos);
}
```
Here we just retrieve the `Todos` from the `fileContext`, and return the collection. The method is marked `async`, even though we don't have any asynchronous code here.
This is future proofing, because when we change to retrieving data from a server, that will be asynchronous.

### Get todo by id
This method should return a Todo by its id:
```csharp
public Task<Todo> GetById(int id)
{
    Todo byId = fileContext.Todos.First(t => t.Id == id);
    return Task.FromResult(byId);
}
```
The `First()` method takes a predicate and returns the first Todo, which matches the criteria. We know the Id is unique, so there should be no problems here.

### Add Todo
In this method we need to implement the functionality of auto-setting the Id of the provided Todo item. The method looks like this:
```csharp
public Task<Todo> AddAsync(Todo todo)
{
    int largestId = fileContext.Todos.Max(t => t.Id);
    int nextId = largestId + 1;
    todo.Id = nextId;
    fileContext.Todos.Add(todo);
    fileContext.SaveChanges();
    return Task.FromResult(todo);
}
```
First, we use the `Max(...)` method to find the largest value of Id in the collection.  
Then we create the new Id, by incrementing the current largest Id by 1.  
We assign that Id to the provided `todo`.  
We add the `todo` to the `fileContext`.  
We call `SaveChanges()` so that the Todos are written to the file.  
Finally, we return the finalized todo object, now with a correct Id, in case it is needed.


### The other methods
Give the other methods a try on your own, and look up the [solution on GitHub](https://github.com/TroelsMortensen/BlazorTodoApp/blob/Part1/FileData/DataAccess/TodoFileDAO.cs) if needed.

**DeleteAsync**: Remove a todo from the collection of todos, based on the given id, in the `fileContext`, remember to call `SaveChanges()`.\
Hint: this method does not return any value, but it still must return a `Task`. You can do this with `return Task.CompletedTask`. 

**UpdateAsync**: First find the existing Todo, by using `todo.Id`. Take the `OwnerId` and `IsCompleted` from the argument, and overwrite the values of the Todo from the `fileContext`. Then call `SaveChanges()`
