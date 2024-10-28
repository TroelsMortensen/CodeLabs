# Updating an existing entity

For this we have two approaches, we could call them "update" and "replace", or "tracked" vs "untracked".

## Updating a tracked entity
The way this works is
1) Retrieve the entity
2) Change some property
3) Save the changes

Whenever we retrieve an entity, EFC will keep a reference to it, so it can detect changes, and optimize the sql to execute against the database.\
It will only update the changed properties.

It works like below, where I update the price of a book.

```csharp
Book found = await ctx.Books.SingleAsync(b => b.Id == 1);
found.Price = 235.00m;
await ctx.SaveChangesAsync();
```

I can of course make multiple updates to a single entity, change the title, etc.

I can also retrieve multiple entities, update them, and call SaveChanges at the end,
so that all the updates happen together in a single transaction.\
A use case for this could be a bank transfer, 
where you need to increase the amount of money in one account, and decrease it in another account.\
Both updates should succeed in the database.

## Updating untracked entity

Sometimes it feels unnecessary to first retrieve an entity to update it.

What you can do instead is:
1) Create a new entity with your new property values
2) Set the Id to that of an existing entity
2) Use the Update method
3) Save the changes

If I want to update my existing book, it looks like this:

```csharp
using AppContext ctx = new();
Book bookToUpdate = new Book
{
    Id = 1,
    Title = "Entity Framework Core In Action",
    PublishDate = new DateOnly(2021, 3, 1),
    Price = 235.00m
};

await ctx.SaveChangesAsync();
ctx.Books.Update(bookToUpdate);
```

You could consider this a "replace" instead. 
EFC will not optimize the query to only set the changed properties, 
it will overwrite all values in the row. 