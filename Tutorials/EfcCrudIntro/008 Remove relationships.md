# Removing relationships between entities

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