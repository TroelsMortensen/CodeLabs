# Add DbContext
The DbContext subclass is your gateway to the database. 

Create such a class, call it whatever you want. `AppContext` is a good start.

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