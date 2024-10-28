# Updating relationships between entities

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

## Remove relationships
Again, we have several different cases and different approaches.


### Dependent relationships
For example, a PriceOffer cannot live without referencing a Book, so we can't just set the PriceOffer::BookId to null.

In this case, I can do either:
1) Delete the PriceOffer
2) Load the book, set the PriceOffer nav prop to null, and let the database delete the PriceOffer for me.
This works, because the database has set the ON DELETE behaviour to CASCADE

The first case is covered in the previous slide about deleting entities.

The other case would look like this:

```csharp
using AppContext ctx = new();

Book book = await ctx.Books
    .Include(b => b.PriceOffer)
    .SingleAsync(b => b.Id == 1);

book.PriceOffer = null;

await ctx.SaveChangesAsync();
```

Notice the Include() call. This will load the Book entity, and _also_ its associated PriceOffer.
If I don't include the PriceOffer like this, the property will just be null.\ 
This is the case for _all_ navigation properties. 
If you wish to load them as well, you must do so explicitly.

And, for some reason, after the Include, the Find method is no longer available, 
so I need to use another way to extract a single entity.

### Independent relationships
Next, let's look at the relationship between category and book again. 
This time, I just want to "break the link" between them, but still keep both entities.

Again, I can load one entity, including the navigation proporty, and remove the other entity from the list.

I have three ways:
1) Remove from list by id
2) Remove from list by reference
3) Optimization to only include relevant entity

#### By Id
I load one entity, include the navigation property entities, and remove one by id:

```csharp
using AppContext ctx = new();

Book book = await ctx.Books
    .Include(b => b.Categories)
    .SingleAsync(b => b.Id == 1);

book.Categories.RemoveAll(cat => cat.Name == ".NET");

await ctx.SaveChangesAsync();
```

#### By entity
Load both entities, remove one from the other:

```csharp
Book book = await ctx.Books
    .Include(b => b.Categories)
    .SingleAsync(b => b.Id == 1);
Category cat = await ctx.Category.SingleAsync(c => c.Name == ".NET");

book.Categories.Remove(cat);

await ctx.SaveChangesAsync();
```

#### Optimization
Now, consider the following. In both of the above cases, I have loaded the Book, and also _all_ associated Categories.\
In this case it's not much of a problem, as the number of categories for a book will probably never grow larger than a handful.\
But for example the number of reviews could be thousands.\
Do I really need to load _all_ reviews, just to remove a single? 
(Well, like PriceOffer, Review cannot exist by itself, so I could just delete the review directly. 
But, just pretend for a moment here that a review could exist without a book. I could set the FK to null, though.
But for the sake of this example..)

The solution? The Include method can consider some filtering, so only _some_ associated entities are loaded.

It looks like this:

```csharp
using AppContext ctx = new();

Book book = await ctx.Books
    .Include(b => b.Categories.Where(cat => cat.Name == ".NET"))
    .SingleAsync(b => b.Id == 1);
Category cat = await ctx.Category.SingleAsync(c => c.Name == ".NET");

book.Categories.Remove(cat);

await ctx.SaveChangesAsync();
```

Now, only categories which match my predicate `cat => cat.Name == ".NET")` are loaded. 
So, instead of loading potentially thousands of irrelevant entities, I only load the one I need to remove.