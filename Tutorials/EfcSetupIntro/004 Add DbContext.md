# Add DbContext
The DbContext subclass is your gateway to the database. 
On second semester, you may have had a DatabaseManager class, which handled all interactions
with the database.\
Maybe you applied DAO pattern, and had multiple DatabaseManagers.\
The DbContext has a similar responsibility.

The EFC package you have added to the project includes a class called `DbContext`. 
This is an abstract class, and you need to create a subclass of it to use it.

The DbContext contains a bunch of useful methods, which we can either use or override.


First, we need to sub-class the DbContext.\
Create a new class, call it whatever you want. `AppContext` is a good start.

> **Edit:**
> 
> I've learned, that there is another existing class called AppContext, 
which sometimes causes conflicts. Maybe another name is better, like `BookstoreContext`.
My examples, though, will still use the AppContext name.


Here's the initial class.

```csharp
public class AppContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = bookstore.db");
    }
}
```

The `OnConfiguring` method is where you (can) specify the database provider and connection string. With SQLite we just point to a file, i.e. `bookstore.db`.

In this case the connection string is hardcoded, and you may prefer to put it in a configuration file. It should be easy to google how to do this.

Later, as we start configuring the domain model, we will add `DbSet` properties to this class. Each DbSet represents a table in the database.