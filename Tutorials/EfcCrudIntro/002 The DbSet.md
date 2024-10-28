# The DbSet

Each DbSet is a "virtual" representation of a database table.

If you have a table in the database called Books, there will be a corresponding DbSet in the DbContext called Books.

This DbSet is our entry point into the database table.

My current DbContext looks like this:

```csharp
public class AppContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<PriceOffer> PriceOffers => Set<PriceOffer>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Writes> Writes => Set<Writes>();
    public DbSet<Category> Category => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = bookstore.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasKey(c => c.Name);

        modelBuilder.Entity<Writes>()
            .HasKey(w => new { w.BookId, w.AuthorId });
    }
}
```

I misspelled the Category DbSet variable, it should have been Categories. Plural. I don't care enough to fix it.

## Repository
The DbSet is a repository pattern, meaning it is an interface for data access, which looks like a list.

Therefore, we interact with the DbSet in much the same way as we would a List, using similar methods, for example: Add, Remove, etc.

We make changes to the DbSet (list), or the entities within the DbSet, and when we make a call to DbContext::SaveChanges(),
EFC will figure out what is changed/updated/deleted, and apply the same changes to the database.


## Transactions
If multiple changes are made to multiple DbSets, and then at the end, we call SaveChanges(), 
all changes will be saved in _one_ transaction.

This means, that if one thing fails to be saved, all changes are discarded.

## Change tracking
The DbSet has a "Change tracker".

This means, all changes made to a DbSet is tracked, all changes to entities within the DbSet is also tracked.

When we save the changes, EFC will figure out how best to execute the changes to the database, 
whether this mean adding, updating, deleting, or replacing rows.

EFC will also figure out the correct order of things.

Whenever we load an entity from the database, this entity is kept in the corresponding DbSet, 
and all changes are then tracked by EFC. This is most evident when we make updates to tracked entities, 
which is shown later. 