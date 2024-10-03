# Primary keys
As mentioned, if an entity class has a property named `Id` or `<classname>Id` of type int, 
EFC will automatically treat it as the primary key.

However, sometimes you may want another type of primary key, or just name it differently.\
Maybe your User has an `Email` property, and you want to use that as the primary key.

In our example, the Category entity has a `Name` property, which we want to use as the primary key.

### Defining single attribute primary key

It can be defined either with an attribute, like this:
```csharp
public class Category
{
    [Key]
    public string Name { get; set; } = null!;
}
```

_Or_ with the Fluent API, like this:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>().HasKey(c => c.Name);
}
```

This method above goes into the DbContext sub-class, i.e. the AppContext in our example. The method is called when creating a migration, and we can configure A LOT of things here.\
In the above case, we just use the ModelBuilder class to say that the Entity of type Category has a primary key, which is the Name property.

Personally, I prefer the Fluent API approach, as it keeps the entity classes cleaner, and the configuration in one place. 
The Fluent API is also more powerful, 
so as your model grows, it is more convenient if all configuration is in one place, rather than split between attributes and Fluent API.  

### Defining composite primary key
Sometimes you need multiple properties/attributes to be included in the primary key. In our example, the Writes relationship attribute will need this.

In step 12 I update the Writes class to include foreign keys to Author::Id and Book::Id. These are used in the composite primary key.

Here is the updated `OnModelCreating` method after step 12:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>()
        .HasKey(c => c.Name);

    modelBuilder.Entity<Writes>()
        .HasKey(w => new { w.BookId, w.AuthorId });
}
```

The second statement says that the Writes entity has a primary key consisting of Writes::BookId and Writes::AuthorId.