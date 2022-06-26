# File Data

We are going to store the data as a string in json format. We could use binary, but sometimes it is just nice to be able to inspect the data in storage.

We are going to need three classes for this: Two DAO classes, and a "Context" class. This latter class will be the one responsible for reading and writing to/from the file.\
We do it like this, because it will be very similar to how we are later going to use Entity Framework Core.

## Data container

First, we will create a class to hold the data. Having all data in one class makes it easier to write it to a file.

Create a class: `DataContainer`.

It looks like this:

```csharp
public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Todo> Todos { get; set; }
}
```

The point is, we will read data from the file and load into these two collections. The collections are essentially our database tables. If we were to need more entities in the future, e.g. Category, we would add more collections.

We could use IList, List or other types of collections, but the Collection will behave similar to how we can interact with the database later on, using Entity Framework Core. So we use ICollection to prepare.

## File Context

This class is responsible for reading and writing.

First, we create the FileContext in the FileData project. 

The final version of the class can be found !here! (insert link).

### Fields

You need to define the path to the file, which should hold the data. And we need two collections, one for Users and one for Todos.

```csharp
private readonly string filePath = "data.json";

private DataContainer? dataContainer;

public ICollection<Todo> Todos
{
    get
    {
        if (dataContainer == null)
        {
            LoadData();
        }

        return dataContainer.Todos;
    }
}

public ICollection<User> Users
{
    get
    {
        if (dataContainer == null)
        {
            LoadData();
        }

        return dataContainer.Users;
    }
}
```

Line 1 is just the file path.\
Line 3 is the DataContainer, which after being loaded, will keep all our data. It is obviously not very efficient or scalable, because we are essentially keeping the entire database in memory. If the database contains a lot of data, we will not have enough memory. However, for this toy example, it is just fine.

Then two properties. They both check if the DataContainer is `null`, meaning that is has not been loaded from the file. If it is `null` we call a currently-not-existing method: `LoadData`, which will read from the file.\
Then the relevant collection is returned.

### Load Data

### Save Changes

### Seed Data



