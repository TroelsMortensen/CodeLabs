# Non-nullable Multi-valued Value Object

This case covers how to configure a non-nullable property value object, containing more than one value.

It is assumed neither the property on the entity, nor the properties on the value object, can be null. Nothing can be null.

We will again use the complex type, as all is non-nullable. Once even a single property becomes nullable, we must revert to using owned entity types, on the next slide.

### Value object

This time, the value object has two properties (or more), each property based on a primitive type.\
If you want to nest value objects, look for a later slide.

```csharp
public class ValueObjectP
{
    public string First { get; }
    public int Second { get; }

    public static ValueObjectP Create(string first, int second) 
        => new ValueObjectP(first, second);
    
    private ValueObjectP(string first, int second) 
        => (First, Second) = (first, second);
    
    private ValueObjectP(){}
}
```
Not much new. But now we have two properties. I have picked types of string and int, just to show two different types. As mentioned, any primitive type is valid here: string, bool, int, etc.

(No, string is not really a "primitive" type, but in these cases, for our purposes, the behaviour is the same).

### Entity
```csharp
public class EntityP
{
    public Guid Id { get; }
    internal ValueObjectP someValue;

    public EntityP(Guid id)
    {
        Id = id;
    }

    public void SetValue(ValueObjectP v) => someValue = v;
}
```

Same same.

### Configuration
Again, we use the complex type to configure this. It looks like this:

```csharp
private void ConfigureNonNullableMultiPrimitiveValuedValueObject(EntityTypeBuilder<EntityP> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder.ComplexProperty<ValueObjectP>("someValue", propBuilder =>
    {
        propBuilder.Property(valueObject => valueObject.First)
            .HasColumnName("First");

        propBuilder.Property(valueObjectP => valueObjectP.Second)
            .HasColumnName("Second");
    });
}
```

First, the Id. As usual.

Then, we define there is a `ComplexProperty` of type `<ValueObjectP>` on EntityP. 
The field variable is called "someValue", and the second parameter, the lambda expression,
defines how to configure this value object.\
We use the PropertyBuilder to first let EFC know about `valueObject.First`, and we rename the column for the database.\
And then we do the same for the second property.

You may see examples online, where they also define these properties as "required", but that's redundant.

### Test
Finally, the test:

```csharp
[Fact]
public async Task NonNullableMultiPrimitiveValuedValueObject()
{
    await using MyDbContext ctx = SetupContext();
    EntityP entity = new(Guid.NewGuid());
    ValueObjectP valueObject = ValueObjectP.Create("Hello world", 42);
    entity.SetValue(valueObject);

    await SaveAndClearAsync(entity, ctx);

    EntityP retrieved = ctx.EntityPs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(valueObject.First, retrieved.someValue.First);
    Assert.Equal(valueObject.Second, retrieved.someValue.Second);
}
```
* Create the DbContext
* Create the entity
* Create the value object
* Add the value object to the entity
* Save the entity, clear the change tracker
* Retrieve the entity
* Assert stuff about the field variable.

I leave it to the reader to create a test proving you cannot save an entity without a value for the field variable, see slide 11.


### Sources
https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew#value-objects-using-complex-types
