# Adding relationships between entities

This topic can be a bit comprehensive, and I have probably forgotten a case or two.

There are several different ways to bind entities together, or to remove existing relationships.

## Add new entity to existing

Let's look at the case of adding a Price Offer to our book.

We have at least two approaches:

1) Attach new PriceOffer to Book's navigation property:
   * Create PriceOffer instance
   * Retrieve book
   * Set book nav prop to point to price offer
2) Set foreign key
   * Create PriceOffer
   * Set the foreign key property value to that of the book's primary key

#### Use nav prop
The first case looks like the following:

```csharp
using AppContext ctx = new();
PriceOffer po = new()
{
    NewPrice = 25m,
    PromotionalText = "On sale!"
};
Book? book = await ctx.Books.SingleAsync(book => book.Id == 1);
book.PriceOffer = po;
await ctx.SaveChangesAsync();
```
After running the above piece of code, we can inspect the PriceOffer table, 
and see that both the primary key and foreign key are updated correctly:

![img_2.png](img_2.png)

The PriceOffer gets the next available Id, i.e. 1, and the foreign key points to the book with Id 1.

#### Use foreign key
Alternatively, instead of retrieving the book, 
we can reduce the number of queries against the database by just setting 
the foreign key property on the PriceOffer entity, like this:

```csharp
using AppContext ctx = new();
PriceOffer po = new()
{
    NewPrice = 25m,
    PromotionalText = "On sale!",
    BookId = 1
};
await ctx.PriceOffers.AddAsync(po);
await ctx.SaveChangesAsync();
```
Notice the `BookId` property is set, and I just add the PriceOffer through its DbSet.

## Add existing entity to another existing
Let's assume we have some categories already, and we want to update an existing book to be part of an existing category.

This is done as follows:
1) Load one of the entities (doesn't matter which one)
2) Load the other entity
3) Add one entity to the collection navigation property of the other (again, it doesn't matter which is which)
4) Save changes

And the code looks like this:

```csharp
using AppContext ctx = new();

Book? book = await ctx.Books.FindAsync(1);
Category? cat = await ctx.Categories.FindAsync(".NET");
book.Categories.Add(cat);

await ctx.SaveChangesAsync();
```

Notice that Find returns a nullable entity, so I _should have_ checked that neither is null.

In my case I _know_ both entities exist, and decided to keep the example simple.

In this case we don't have any foreign keys, so the above approach is the only available.