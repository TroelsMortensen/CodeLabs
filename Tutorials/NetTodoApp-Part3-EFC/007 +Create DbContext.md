# Create DbContext

You currently have a FileContext class, in FileData component, which is responsible for providing collections of Todo and User. It also loads data, and saves changes.

The DbContext has a similar responsibility. Furthermore, it is here we define how the database should look like. Sort of.

Create a new class, "TodoContext" (or AppContext, or whatever), inside EfcDataAccess component. It must inherit from `DbContext`, which is available after the installation of the NuGet packages, slide 5.

## Specifying the Database

We need to specify which database to use. That's done in the inherited method `OnConfiguring(...)` (there are other approaches, we keep it simple, here).

The class then currently looks like this:

```csharp
public class TodoContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Todo.db");            
    }
}
```

We have defined two `DbSets`. When the database is generated, it will result in a table per DbSet, so we get a Todos table and a Users table.

We interact with the DbSet in a similar way to how we used the Collection of the FileContext. 

We interact with this DbSet to add, get, update, remove Todos/Users from the database.

The `OnConfiguring(...)` method is here used to specify the database to be used. This is done with the method `UseSqlite(...)`. This method is available because we added a NuGet package for SQLite.
If we were using Postgres, we would have a different method here, probably `UsePostgres(..)`.

The argument is the name of the SQLite database file: Todo.db.\
For SQLite it is simple, there is no authentication, there is no actual database server running, so no IP or port. The "connection string" here is just a reference to the file. 
For other databases, you will have to provide a more elaborate connection string.

[I have an example on GitHub, where I connect to a local Postgres database, and a cloud-hosted Postgres database.](https://github.com/TroelsMortensen/EFCpostgres)

SQLite is just a single file, so that makes it easier to work with, instead of having to use Postgres or MySql or similar. And the argument of `UseSqlite(..)` is just pointing to this file.

You may sometimes need to specify the absolute path to the db file, e.g.:

```jsonpath
C:\TRMO\RiderProjects\TodoAppWasm\EfcDataAccess\Todo.db
```

This is _probably_ because your main method is in one project, and the file is in another.

Alternatively a relative path should be possible too, something like:

```jsonpath
..\EfcDataAccess\Todo.db
```

### Note

The above method is a simple approach, however we have now hardcoded the database info, 
and it may not be easy to modify. 

Usually the connection info will go into a configurations file, and the program will read from that. This provides the option of being able to change the connection string after the program is compiled and deployed. 

Furthermore the `optionsBuilder.UseSqlite` can be done in Program.cs, so that it is easier to modify which database is used, without having to touch the `TodoContext` class. This increases flexibility, if you ever wish to change database. We don't, so we keep it simple.

![img.png](Resources/KISS.png)

It is left to the reader to google how to do that, if they're interested.

## Other Database Providers
If you wanted to use a different DBMS, e.g. Postgres, you would add a NuGet package for a Postgres driver. 

That would then include a method `UsePostgres(...)`, in which you would provide connection arguments.