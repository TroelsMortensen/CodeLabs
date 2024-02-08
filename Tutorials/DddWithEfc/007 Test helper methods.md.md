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

These two methods will be used in each of my tests going forward.

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
In a real system, you often have different databases, like production, Q/A testing, local testing, etc. We need a database, we can reset for every test.\
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
The next helper method covers saving an entity, and clearing the ChangeTracker (the cache).

```csharp
private static async Task SaveAndClearAsync<T>(T entity, MyDbContext context) 
    where T : class
{
    await context.Set<T>().AddAsync(entity);
    await context.SaveChangesAsync();
    context.ChangeTracker.Clear();
}
```

Line 1:
* Again, static, performance, not really necessary, but Rider gives warning.
* Method is generic `<T>`, so that it can save any type of entity. That's the first argument, `T entity`.

Line 2: The last part is a constraint on the generic type argument `T`, 
saying the argument must be a class. 
Not a struct or record struct. 
We need this constraint, because this constraint is also on the `Set<T>()` method further down.

Line 4: Access the DbSet containing whatever type of entity `entity` is.\
Line 5: Save everything to database.\
Line 6: Clear the ChangeTracker, i.e. cache.