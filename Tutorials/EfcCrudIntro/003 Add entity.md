# Add an entity

There are two ways to add a new entity:
* The DbSet
* The generic Set

They work very similar.

## Add book
I will add a new book to the database.

I first create a new instance of the Book:

```csharp
Book book = new Book
{
    Title = "Entity Framework Core In Action",
    PublishDate = new DateOnly(2021, 3, 1),
    Price = 249.95m
};
```
You may notice the 'm' in the last variable, this is just to indicate this is a decimal, to match the type of the property 'Price'.

Notice also, I don't set the Id property of the Book. This is because the property is of type int,
and by convention EFC will configure this as auto-generated.\
So, when I don't provide a value, EFC will generate it.\
If I to provide a value, EFC will just use that, if it is not already in use in the table.

Then I use the DbSet<Book> to add the new book, and then save the changes.

```csharp
using AppContext ctx = new();

await ctx.Books.AddAsync(book);
await ctx.SaveChangesAsync();
```

The `using` in front of the instantiation of the AppContext means that when the DbContext is no longer used,
it will be disposed of. This includes terminating the connection to the database.

And that's it.

If you run this, you can now open up the database view, refresh it, and see your new row.\
You should also see the Id has been generated. Like this:

![img_1.png](img_1.png)

