# Entities
In order to keep the following steps simpler, we will first implement the entities without any relationships. We will add the relationships in the following steps.

I am being lazy, and I have put all my entities into the same file. Generally you probably want an Entities directory, with a file per entity class.

## General comments

## Book
```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateOnly PublishDate { get; set; }
    public decimal Price { get; set; }
}
```

Here's the Book entity. 