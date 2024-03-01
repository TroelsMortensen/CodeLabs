# Non-Nullable Nested Value Objects

This case is about a value object containing one or more value objects. 
And, all are **non-nullable**.

We can go with the complex type again.

### Value objects

This time, we need three value objects. One value object contains two other value objects.

Here's the first, top-level value object:
```csharp
public class ValueObjectR
{
    public NestedValueObjectR1 First { get; }
    public NestedValueObjectR2 Second { get; }

    public static ValueObjectR Create(NestedValueObjectR1 first, NestedValueObjectR2 second)
        => new ValueObjectR(first, second);

    private ValueObjectR(NestedValueObjectR1 first, NestedValueObjectR2 second)
        => (First, Second) = (first, second);

    private ValueObjectR()
    {
    }
}
```
You will notice that the properties are of type `NestedValueObjectR1` and `NestedValueObjectR2`. 
These are the two nested value objects.

Here is then the first nested value object:
```csharp
public class NestedValueObjectR1
{
    public string Value { get; }

    public static NestedValueObjectR1 Create(string input)
        => new NestedValueObjectR1(input);

    private NestedValueObjectR1(string input)
        => Value = input;

    private NestedValueObjectR1()
    {
    }
}
```
It has a string property. The next nested value object is similar:
```csharp
public class NestedValueObjectR2
{
    public int Value { get; }

    public static NestedValueObjectR2 Create(int input)
        => new NestedValueObjectR2(input);

    private NestedValueObjectR2(int input)
        => Value = input;

    private NestedValueObjectR2()
    {
    }
}
```
It just has an int property instead. Just to show different types.

### Entity
The entity looks familiar by now:

```csharp
public class EntityR
{
    public Guid Id { get; }

    internal ValueObjectR someValue;

    public EntityR(Guid id)
        => Id = id;

    public void SetValue(ValueObjectR v) => someValue = v;
}
```
It contains a non-nullable instance of the top level value object, `ValueObjectR`.


### Configuration
Configuration is done with the complex type approach. 

```csharp
private void ConfigureNonNullableNestedValueObjects(EntityTypeBuilder<EntityR> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder.ComplexProperty<ValueObjectR>("someValue", propBuilder =>
    {
        propBuilder.ComplexProperty(x => x.First)
            .Property(x => x.Value)
            .HasColumnName("First");

        propBuilder.ComplexProperty(x => x.Second)
            .Property(x => x.Value)
            .HasColumnName("Second");

    });
}
```
We define the key.

Line 5: We use `ComplexProperty` to define the nested value object. 
Inside, we then configure the nested value objects, also as complex types.
Again, we target the "someValue" field variable, of type "<ValueObjectR>".

We then define each of the nested value objects as a complex type, e.g. in line 7 and 11.

The first says that ValueObjectR contains a complex type, i.e. the "First" property, which has a property called "Value".\
We redefine the column name to "First". Otherwise this would be "someValue_First_Value", which looks annoying.

Then we do the same, for the second nested value object.

The outputted sql looks like this:

```sql
CREATE TABLE "EntityRs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityRs" PRIMARY KEY,
    "First" TEXT NOT NULL,
    "Second" TEXT NOT NULL
);
```

Here we can see that the entire value object hierarchy has been flattened. The top level value object is actually "gone". Though, if it had a primitive type property, we would configure that (maybe in a later edition of this guide //TODO ).

### Test
Man, I am getting tired of writing tests. But here we go again. I'll skip the "value is null test". _You_ shouldn't do that, obviously, _you_ should be thorough!

```csharp
[Fact]
public async Task NonNullableNestedValueObject()
{
    await using MyDbContext ctx = SetupContext();
    EntityR entity = new(Guid.NewGuid());
    NestedValueObjectR2 nested1 = NestedValueObjectR2.Create(42);
    NestedValueObjectR1 nested2 = NestedValueObjectR1.Create("Hello world");
    ValueObjectR valueObject = ValueObjectR.Create(nested2, nested1);
    entity.SetValue(valueObject);
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityR retrieved = ctx.EntityRs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(valueObject.First.Value, retrieved.someValue.First.Value);
    Assert.Equal(valueObject.Second.Value, retrieved.someValue.Second.Value);
}
``` 
This test sets all values to non-null, and asserts that the values are retrieved correctly.