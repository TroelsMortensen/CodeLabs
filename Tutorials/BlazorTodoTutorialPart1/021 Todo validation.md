# Todo validation rules
Now that we can view all Todos, it's time to add new Todo items.

First, we want to make sure, that whenever we add a Todo, the data is valid.

We can do this by adding Validation attributes to the `Todo` class, like so:

```csharp
public class Todo
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int OwnerId { get; set; }

    [Required, MaxLength(128)] 
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}
```

**Line 5**: This is an attribute, an extra piece of information defined in [ ]. We will use various attributes throughout the course.
This one says that the valid values for `OwnerId` is 1 to the maximum value of `int`. We also provide an error message, these are used by various frameworks.

**Line 8**: Here we have two attributes, Required and MaxLength. They could have been split into `[Required] [MaxLength]`, if you prefer. 
Required means that this property is not allowed to be `null`. MaxLength means that the `Title` can contain a maximum of 128 characters.

We don't have any requirements for `IsCompleted`, so the default value will just be `false`.

There are various validation attributes, you can google for examples. They are used by certain Blazor components to validate data, and later, they are used by the Web API

### Empty constructor
For the next part, we are also going to need an empty constructor in `Todo`:

```csharp
public Todo() { }
```

This is need by the upcoming blazor page.