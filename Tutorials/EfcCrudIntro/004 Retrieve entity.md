# Retrieving an entity

Because of the way EFC and the repository DbContext works, we often have to retrieve entities.

This is necessary when we want to update something: First we retrieve the entity, then modify it, then save it.

It is also necessary when deleting: Retrieve the entity, remove it from the DbSet (which represents the table), then save the change.

There are several ways of retrieving a single entity:

* Find
* Single (OrDefault)
* First (OrDefault)

## Find
This method is used to search for an entity by its Id. The argument is a list of the primary key attribute values, i.e an `object[]`.

For example:

```csharp
Book? found = await ctx.Books.FindAsync(1);
```

Notice that if no matching book was found, `null` is returned. You will have to deal with this null-case somehow.
Either throw an exception, or your specific case might have an other relevant way of handling the null-case.

The Find() method is only available on the DbSet, it seems, but not on the more general IQueryable. This will become evident in slide 7.

## Single (OrDefault)
This method can search for entities by other candidate keys than the primary key.\
You just provide a lambda expression as the search criteria.

Like this:

```csharp
Book found = await ctx.Books.SingleAsync(b => b.Id == 1);
```

The above looks for a book, where the Id is 1. If my entity had other unique properties, like user::email, I could search by email too.

With the Find() method, the call results in one of three cases:
1) A book was found, and returned.
2) No matches was found, and an exception is thrown.
3) Multiple matches were found, and an exception is thrown.

#### OrDefault
If you don't like to potentially have to catch an exception, 
you can use the `SingleOrDefault` method,
which will return null, if no matches were found, i.e. case 2) above. Case 1) and 3) is the same.

Now you just have to handle the case of the entity being null, and potentially throw an exception, or somehow deal with this situation.

## First (OrDefault)
This method is similar in it's use, you provide a lambda expression as the search criteria.

```csharp
Book? found = await ctx.Books.SingleOrDefaultAsync(b => b.Id == 1);
```

But, the potential resulting outcomes are slightly different:
1) A book was found, and returned.
2) No matches, an exception is thrown.
3) Multiple matches, the first match is returned.

So, this method returns the first match, while the Single() ensure there is _only_ one match.\
This means that First() is probably faster, but Single() is both safer, and clearer in its intend.

#### OrDefault
Again, there is an alternative, which returns null in case of no match, rather than throwing an exception.

## Generic Set\<T\>
In the above, we have used the DbSet. I have also stated in the other tutorial that you don't strictly need to always define a DbSet.

Instead you can use the generic set, which acts exactly in the same was.

The two below lines of code do the same thing:

```csharp
Book found = await ctx.Books.SingleAsync(b => b.Id == 1);
Book found1 = await ctx.Set<Book>().SingleAsync(b => b.Id == 1);
```

Generally, using the DbSet is probably slightly easier. However, the generic approach opens of for interesting options, like defining a generic implementation of your own repository classes, which can work for all entities.\
You could play around with this in your assignment. It means, you can create _one_ repository class, which can work for any current and future entity.