# Adding DbContext

In your new component, EfcData, add a new class. You could name it TodoContext.\
This class will have a responsibility similar to your FileContext class, i.e. provide interaction with the data storage.

It must inherit from DbContext.



In this class you define DbSets for the object types, you want to be able to access in your database.

In this tutorial, we just have the `Todo` object, but you might also have a `User` object. If the project scaled up, we might have different `TodoList`s, owned by different Users. Maybe you'll add this later.


## Specifying the Database

We need to specify which database to use. That's done in the inherited method `OnConfiguring(...)`.

The class currently looks like this:

```csharp
public class TodoContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Todo.db");
    }
}
```

The `DbSet<Todo>` represents the Todo table in the database. The DbSet looks a bit like the ICollection, with regards to available methods, which is why we have used ICollection a lot so far.

We interact with this DbSet to add, get, update, remove Todos from the database.

The `OnConfiguring(...)` method is here used to specify the database to be used. This is done with the method `UseSqlite(...)`.\
The argument is the name of the database: `Todo.db`.

Sqlite is just a single file, so that makes it easier to work with, instead of having to use Postgres or MySql or similar.

#### Note
The above method is a simple approach, however we have now hardcoded the database info, and it may not be easy to modify. Usually the connection info will go into a settings file, and the program will read from that. It is left to the reader to google how to do that, if they're interested.

### Other database providers
If you wanted to use a different DBMS, e.g. Postgres, you would add a NuGet package for a Postgres driver. That would then include a method `UsePostgres(...)`, in which you would provide connection arguments.

## Configuring Todo table

Now, we wish to configure the Todo class a bit.

It currently has a couple of attributes on the properties, like `[Range..]` or `[Required]`. These are converted to constraints in the database, so that's a good start.

We need to define a Primary Key for the Todo table. This can be done in multiple ways:

1) Have an `int` property named `Id`, or `[Class-name]Id` e.g. `TodoId`. Such an attribute will automatically be made Primary Key
2) Add the `[Key]` attribute to the existing `Id` property of `Todo`. This is necessary if the property is named differently. It may also be necessary if the property is not an int.
3) We can configure a lot of things in the TodoContext class, i.e. outside of the Todo model class.

So which approach to use? It may not matter much, but you may also have preferences.

Personally, I don't like the automatic detection, i.e. just have a property called Id. It seems fragile.

So, at least add the `[Key]` attribute:

```csharp
public class Todo
{
    [Key]
    public int Id { get; set; }
    
    ...
```

But, now we have added something to our Model class, which is only there, because we use EFC. We have added a dependency from the Domain layer to the Data layer. This may not be a good approach.

1) It requires modifications of classes outside of the data access layer
2) If you later wish to not use EFC, we must again modify classes outside the data access layer, i.e. the `[Key]` attribute is no longer needed, and should be removed.

Adding `[Key]` is simple, and can be just fine. 

Alternatively, we can set up the keys in the DbContext class:

## On Model Creating

Inside your TodoContext, you can inherit a method, `OnModelCreating`. This method can be used to specify all kinds of things, e.g. 
* primary keys
* foreign keys
* composite keys
* constraints

Add the following method:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Todo>().HasKey(todo => todo.Id);
}
```

This says that the Entity `Todo` has a Key, defined as the property `Todo.Id`.\


##### Extra note
Should we wish a composite key, i.e. a primary key consiting of more properties, we will have to use this approach.

As a simple example, we could do:

```csharp
modelBuilder.Entity<Todo>().HasKey(todo => new {todo.Id, todo.Title});
```

Which will make a composite key of `Id` and `Title` from the todo.