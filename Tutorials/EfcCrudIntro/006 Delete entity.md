# Delete existing entity

The way this works is to retrieve the entity, remove it, save the changes.

```csharp
using AppContext ctx = new();
Book? book = await ctx.Books.FindAsync(1);
ctx.Books.Remove(book);
await ctx.SaveChangesAsync();
```
