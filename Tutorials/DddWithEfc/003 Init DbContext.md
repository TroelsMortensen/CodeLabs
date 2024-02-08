# Setup DbContext

Now, we create the DbContext. The entry point whenever we wish to interact with the database.

Start out something like this:

```csharp
public class SqliteWriteDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<VeaEvent> Events => Set<VeaEvent>();
    public DbSet<Guest> Guests => Set<Guest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
```

Explanation for lines:

Line 1
* I have called the class SqliteWriteDbContext. You will later have a similar "Read" context.
* We get `DbContextOptions` through the [Primary Constructor](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors)
* The `options` argument is passed to the base class. This argument contains information about which database to use.\
It must be injected from the outside because of testing reasons. You will see later.

Lines 3-4
* I define a DbSet _per aggregate **root**_ entity. There should be no need to define sets for entities, other than the root.
* Obviously, if you do a different project case, or have different aggregate root entities, adjust the code accordingly.
* The `=>` part is just the general recommendation, it means we retrieve the specific generic Set, which contains a certain entity.


Lines 6-9: `OnModelCreating()`: This is left empty for now, but here we will add all configuration code.

This should be enough for now.