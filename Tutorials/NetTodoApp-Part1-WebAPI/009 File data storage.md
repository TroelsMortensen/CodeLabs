# File Data

We are going to store the data as a string in JSON format. 
We could use binary, but sometimes it is just nice to be able to inspect the data in storage, or even quickly modify it. 
So, JSON is an easy way to go.

We are going to need three classes for this: Two DAO classes, and a "Context" class. 
This latter class will be the one responsible for reading and writing to/from the file. The two DAO classes will come later, once we need them. We work by one feature at a time.\
We do it like this, because it will be very similar to how we are later going to use Entity Framework Core.

## Data container

First, we will create a class to hold the data. Having all data in one class makes it easier to write it to a file, it is a bit of a hack, and this doesn't scale. We will essentially load all data into memory. But, the JSON storage is just for our initial minimum viable product, or proof of concept.

Inside FileData component create a class: `DataContainer`.

It looks like this:

```csharp
public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Todo> Todos { get; set; }
}
```
Fix import errors.

The point is, we will read data from the file and load into these two collections. 
The collections are essentially our database tables. 
If we were to need more model classes in the future, e.g. Category, Project, or something else, we would add more collections.

We could use `IList`, `List` or other types of collections, 
but the Collection will behave similar to how we can interact with the database later on, using Entity Framework Core. 
So we use ICollection to practice.

## File context

This class is responsible for reading and writing the data from/to the file.

First, we create the `FileContext` class in the FileData project. 

The final version of the class can be found [here](https://github.com/TroelsMortensen/WasmTodo/blob/master/FileData/FileContext.cs).

### Fields

You need to define the path to the file, which should hold the data. And we need two collections, one for Users and one for Todos.

```csharp
private const string filePath = "data.json";
private DataContainer? dataContainer;

public ICollection<Todo> Todos
{
    get
    {
        LoadData();
        return dataContainer!.Todos;
    }
}

public ICollection<User> Users
{
    get
    {
        LoadData();
        return dataContainer!.Users;
    }
}
```

Line 1 is just the file path.\
Line 2 is the DataContainer, which after being loaded, will keep all our data. 
It is obviously not very efficient or scalable, because we are essentially keeping the entire database in memory. 
If the database contains a lot of data, we will not have enough memory. 
However, for this toy example, it is just fine.
Notice the variable is _nullable_, marked with the "?", indicating we allow this field to be null. We will regularly reset the data, clear it out and reload it.

Then two properties. They both attempt to lazy load the data. Then the relevant collection is returned.

The `LoadData` method will check if the data is loaded. If not, i.e. `dataContainer` is `null`, then the data is loaded. See below.

### Load data

We need a method to read from the file, so we can retrieve data.

```csharp
private void LoadData()
{
    if (dataContainer != null) return;
    
    if (!File.Exists(filePath))
    {
        dataContainer = new ()
        {
            Todos = new List<Todo>(),
            Users = new List<User>()
        };
        return;
    }
    string content = File.ReadAllText(filePath);
    dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
}
```

What's going on here?

The method is private, because this class should be responsible for determining when to load data. 
No outside class should tell this class to load data.\
First we check if the data is already loaded, and if so, we return.\
Then we check if there is a file, and if not, we just create a new "empty" DataContainer.\
If there is a file: 
We read all the content of the file, it returns a string.
Then that string is deserialized into a `DataContainer`, and assigned to the field variable.

### Save changes

The purpose of this method is to take the content of the DataContainer field, and put into the file.
```csharp
public void SaveChanges()
{
    string serialized = JsonSerializer.Serialize(dataContainer);
    File.WriteAllText(filePath, serialized);
    dataContainer = null;
}
```
Later, when we work with databases through Entity Framework Core, you will also need to call SaveChanges after interacting with the database. 
So, we practice the workflow here.\
The `DataContainer` is serialized to JSON, then written to the file. Then the field is cleared.

### Efficiency?
We are going to save the Domain objects as they are. This means multiple Todos may reference the same User, and so in the JSON file we will find the same User data multiple times.

Obviously this is not particular efficient, having this duplicate data. It is, however, a flaw we will accept for the JSON storage functionality, as this is just a placeholder until we get the actual database in place.\
This database will be normalized, and we will be rid of duplicate data.

### GitHub
Here ends the first branch on GitHub, the [basic setup](https://github.com/TroelsMortensen/WasmTodo/tree/001_BasicSetup). The next part will be on a new branch.