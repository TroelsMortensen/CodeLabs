# Entities
In order to keep the following steps simpler, we will first implement the entities without any relationships. We will add the relationships in the following steps.

I am being lazy, and I have put all my entities into the same file. Generally you probably want an Entities directory, with a file per entity class.

## Conventions
As mentioned previously, EFC follows a set of convetions/rules to automatically configure everything. Below is a list of the conventions we will follow.

#### Public properties
Generally, you will want to use properties with both set and get, either public or private. For example:

```csharp
public string Name { get; set; }
public string Text { get; private set; }
```
EFC will discover both public and private properties by default.

#### Private constructor
You may have a constructor on your entity class.\
If it takes in arguments for _all_ properties, it should be fine.\
If it takes in arguments for _some_ properties, you need to also include a private no-argument constructor, like this:

```csharp
private Book() { } // Used by EFC
```

EFC will by default instantiate your entity with the private constructor, then use the properties to set the values.

#### Primary Key
A property of type `int` named `Id` will be the primary key.\
Alternatively, the name can also by <class-name>Id. For example, `BookId`.

EFC will automatically configure this property as the primary key.

#### Foreign Key
Foreign keys are included in the database, and are used to link entities together.\
They are automatically created by EFC when you have a navigation property. We will cover navigation properties when setting up relationships.

You may optionally include a property of type int to act as an explicit foreign key on the entity. This is not required, but can be useful in some cases.\
When doing this, the name of the property should be <navigation-property-name>Id. For example, if you have a navigation property called `Author` on the Book, the foreign key property should be `AuthorId`.

## Entities
Below you will find the initial code for each entity, without the relationships specified.


#### Book
Here's the Book entity.

```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateOnly PublishDate { get; set; }
    public decimal Price { get; set; }
}
```

The Id property is autamatically configured as the primary key.\
The Title property is a string, and cannot be null. I assign it to `null!` to suppress the null warning from the compiler.\
The PublishDate is a DateOnly.\
The Price is a decimal.

#### PriceOffer
Here's the PriceOffer entity.

```csharp
public class PriceOffer
{
    public int Id { get; set; }
    public decimal NewPrice { get; set; }
    public string PromotionalText { get; set; } = null!;
}
```
Nothing new here.

#### Author
Here's the Author entity.

```csharp
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
```

#### Review
And the Review entity.

```csharp
public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string VoterName { get; set; } = null!;
    public string? Comment { get; set; }
}
```
Here we have a nullable string. This tells EFC to configure the corresponding column in the database as nullable. Otherwise the default is that the value is required, or `NOT NULL`.

#### Category
And the Category entity.

```csharp
public class Category
{
    public string Name { get; set; } = null!;
}
```
This time we don't have an int Id property.\
We must then explicitly tell EFC that the Name property is the primary key. We will do this later.

#### Writes
Finally, the Writes relationship attribute.

```csharp
public class Writes
{
    public int Order { get; set; }
}
```

There are currently no keys on this. This will require a bit of configuration later, because the standard approach is to use a composite key, from the foreign keys to Book and Author.

Next up, we will add the relationships between the entities.