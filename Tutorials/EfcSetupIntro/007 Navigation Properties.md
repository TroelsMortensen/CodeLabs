# What is a navigation property?
A navigation property is just a reference to another entity. A normal association from one class to another. 
What makes it a _navigation property_ is just that EFC knows about it, and we can use it to navigate from one entity to another.
We will see this when querying data later on.

Navigation properties are generally how we define relationships in EFC, when we do it by convention.

If an entity needs a relationship to another, we add a navigation property to the entity. I.e. an association.\
If the relationship is "many", we add a navigation property of type `List<T>` (or some other collection type).

For 1:1 and 1:* cases, we only have to put the navigation property in one of the entities.\
For the \*:\* case, we need to put a collection navigation property in both entities.\
Generally though, it can be a good idea to put the navigation property in both entities, to make it easier to navigate in both directions.

## Foreign keys
We can also include a foreign key property. Sometimes this make it easier to work with the relationship. It is necessary for the 1:1 relationship. Otherwise it is optional.

## Example
Here's an example where ClassA has a reference to ClassB.

```csharp
class ClassA
{
    public int Id { get; set; }
    public ClassB ClassB { get; set; } // Navigation property
}

class ClassB
{
    public int Id { get; set; }
}
```

This is enough to tell EFC that there is a relationship between `ClassA` and `ClassB`.\

We can add a foreign key property to `ClassA` like this:

```csharp
class ClassA
{
    public int Id { get; set; }
    public int ClassBId { get; set; } // Foreign key
    public ClassB ClassB { get; set; } // Navigation property
}
```

When doing this, the name of the property should be `<navigation-property-name>Id`. 
For example, if you have a navigation property called `Author` on the Book, the foreign key property should be `AuthorId`.

It is a balance between how much you want to "pollute" your entities, and how flexible they are to work with.\
It is often more convenient to have all the foreign key properties, and navigation properties you can.