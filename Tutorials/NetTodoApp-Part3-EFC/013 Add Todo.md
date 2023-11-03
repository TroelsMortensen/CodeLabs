# Add Todo

For this we first need the method `UserEfcDao::GetByIdAsync`. Then the method `TodoEfcDao::CreateAsync`.

## Get User by Id

It should retrieve a User by its Id. You have previously used the method `FirstOrDefault`. You can do that again.\
Or you can use `Find()`, it takes an id of type int.

Give it a go.

<details>
<summary>hint</summary>

```csharp
public async Task<User?> GetByIdAsync(int id)
{
    User? user = await context.Users.FindAsync(id);
    return user;
}
```

</details>

## Create Todo
Then we go to `TodoEfcDao::CreateAsync`, implement this. 

<details>
<summary>hint</summary>

```csharp
public async Task<Todo> CreateAsync(Todo todo)
{
    EntityEntry<Todo> added = await context.Todos.AddAsync(todo);
    await context.SaveChangesAsync();
    return added.Entity;
}
```

</details>

## A problem
We have added two-way navigation properties to the domain classes, i.e. Todo associates User, and User associates Todo.\
The Web API will return JSON. We cannot serialize objects to JSON if there are circular dependencies, which is what we have.

So, two approaches to fixing this:

1) We return a customized DTO instead of the Todo from the Create endpoint.
2) We add the `[JsonIgnore]` attribute to User::Todos property

We will go with the second approach, because this will sort of take us back to the original setup, before adding navigation properties.\
And it causes fewer changes, than having to return a DTO.

So, make this modification:

```csharp{5}
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    [JsonIgnore]
    public ICollection<Todo> Todos { get; set; }
}
```

Last option is to set the JsonSerializerOptions, and tell the serializer to ignore cycles:
```csharp
JsonSerializer.Serialize(myObj, new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles
});
```

## Another problem
Because we are using no tracking for EF Core, the logic part of creating a Todo will fail.

If we disable the no-tracking, it will still work.

I prefer, instead, to change things a little bit.

**First**, you must change the Todo class:

```csharp{5, 10}
public class Todo
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public int OwnerId { get; set; }
    public string Title { get; private set; }

    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
    ...
```
We add an explicit foreign key, the OwnerId. EFC will identify this as an FK because of naming conventions.\
It matches the name of the Owner property, + "Id".

Second, the constructor takes a value for the foreign key, rather than an association to a user.

**Second**, now the TodoLogic complains. In the Create() method, where you instantiate a new Todo, you must now pass the `user.Id` instead:

```csharp
Todo todo = new Todo(user.Id, dto.Title);
```

It is around line 27.

And the Update(), you must do the same. Around line 68, pass the userToUse.Id` instead.

That should be all.

## Test 
.. this functionality through Swagger. It's the POST /Todos endpoint.

You can verify the success through the Database tool, by opening the Todos table. Notice the foreign key is set, because the Todo object had a reference to a User object.