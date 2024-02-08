# Test Helper Methods
Each configuration case is demonstrated with an automated integration test. 

Each test needs to do similar things:
* Instantiate a DbContext
* Create a clean database
* Setup one or more entities with whatever values, matching the specific configuration we are testing
* Save entities to the database
* Clear the [ChangeTracker](https://learn.microsoft.com/en-us/ef/core/change-tracking/) of DbContext. This is keeping track of every entity The DbContext has handled, like a cache. We must clear it, so that data is fetched from the database, _not_ the cache.
* Retrieve the same entity
* Verify the relevant data is retrieved too, as expected.

This must be done for every test. I therefore have two helper methods, to shrink the amount of copy/pasting needed.

### Create DbContext and Setup Database
The first helper method creates a DbContext, and sets up a fresh database:

```csharp
private static MyDbContext SetupContext()
{
    DbContextOptionsBuilder<MyDbContext> optionsBuilder = new();
    optionsBuilder.UseSqlite(@"Data Source = Test.db");
    MyDbContext context = new(optionsBuilder.Options);
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    return context;
}
```
Line 1: It is static for performance reasons, not strictly necessary.\
Line 3: Create options builder, here we can set various db configurations. In our case the specific test database.\
In a real system, you often have different database, like production, testing, local testing, etc. We need a database, we can reset for every test.\
Line 4: We define the test database.\
Line 5: Create new instance of DbContext.\
Line 6: Delete any existing database.\
Line 7: Create clean database.

This method is called at the beginning of each test, like this:

```chspar
await using MyDbContext ctx = SetupContext();
```

The we have a fresh DbContext, with a clean database.

### Save and Clear
The next helper methods covers saving an entity, and clearing the ChangeTracker (the cache)
