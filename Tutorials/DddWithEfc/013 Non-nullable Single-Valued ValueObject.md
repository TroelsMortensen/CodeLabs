# Non-nullable Single Value Value Object

This case covers how to configure a non-nullable property value object, containing a single value.

Here, we use the new feature of EFC8 called complex types.

There are two parts to this: non-nullable, and single-valued.

First the single-valued value object:

### Value object
Now, your value object probably inherits from a base class, and overrides equality. That's great.\
It's not relevant for this example, so my value object is simplified, to this:

```csharp
public class ValueObjectN
{
    public string Value { get; }

    public static ValueObjectN Create(string input) 
        => new ValueObjectN(input);
    
    private ValueObjectN(string input) => Value = input;
    private ValueObjectN(){}
}
```
It contains a single value, i.e. a single property. That value is of a primitive type: string, bool, int, etc.

### Non-nullable
In C# we indicate if a variable (local or field) or property can contain a `null`-value, but appending "?" after.\
Like this:

```csharp
public string? MyProperty { get; set; }
```

Here, we say that the value of this property _may_ be null.

Non-nullable is then without this question-mark, indicating the value must not be null.\
This is the case, we are dealing with here. EFC will look at this "?" regarding not null constraints in the database.

### Entity
Here, then, is my entity:

```csharp
public class EntityN
{
    public Guid Id { get; }

    internal ValueObjectN someValue;
    
    public EntityN(Guid id)
    {
        Id = id;
    }

    public void SetValue(ValueObjectN v) => someValue = v;
}
```

Yes, the value of `someValue` will initially be `null` upon object instantiation.\
But it will not be `null` upon persistence.

### Configuration
In this case, with non-nullability, we can use the complex type feature.

Here's the configuration:

```csharp
private void ConfigureNonNullableSinglePrimitiveValuedValueObject(EntityTypeBuilder<EntityN> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder.ComplexProperty<ValueObjectN>(
        "someValue",
        propBuilder =>
        {
            propBuilder.Property(vo => vo.Value)
                    .HasColumnName("value");
        }
    );
}
```

This method receives an `EntityTypeBuilder` for `EntityN`, i.e. a class used to configure EntityN.
Just like previous examples.

First, we must define the primary key, as always.

Then the interesting part:\
Line 5: We say EntityN has a "Complex Property", of type `<ValueObjectN>`. This method takes two arguments.\
Line 6: The first argument, is the name of the field variable.\
Line 7-10: The second argument is a function. Here is a PropertyBuilder, i.e. a class used to configure a property. We say here the complex type of type ValueObjectN has a property called `Value`.
We have to be explicit because the property Value on ValueObjectN does not have a `set;`. Therefore we must explicitly point EFC to this property.\
Line 10: The name of the column in the database is a combination of the property name on EntityN and the property name on ValueObjectN: "someValue_Value".
This line here renames the column name to something more meaningful. Pick a name which makes sense, when you just look at the database table. Is it a PhoneNumber? FirstName? Email?

What happens when we use complex type? The value object is "flattened" onto the entity. 
Instead of the entity having a reference to a value in a separate table, which would be the case if the value object was treated like an entity,
we then take the value object's value, and put into the entity, in the table.

We get a table called EntityNs, with two attributes: Id and someValue.

You may verify this with the console command "dotnet ef dbcontext script".

The value object becomes part of the entity, which makes perfect sense.

### Test
Here's the test:

```csharp
[Fact]
public async Task NonNullableSinglePrimitiveValuedValueObject()
{
    await using MyDbContext ctx = SetupContext();
    ValueObjectN value = ValueObjectN.Create("Hello world");
    EntityN entity = new(Guid.NewGuid());
    entity.SetValue(value);

    await SaveAndClearAsync(entity, ctx);

    EntityN retrieved = ctx.EntityNs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(value.Value, retrieved.someValue.Value);
}
```

* The value object is instantiated.
* Then the entity.
* The value object is added to the entity.
* Save and clear
* Retrieve entity
* Verify the property is correctly loaded, i.e. not null
* Verify the value of the value object is correct


#### Rainy test
The following test shows that the entity cannot be saved, if the value object property is not set.\
We get an InvalidOperationException thrown from the DbContext, and it provides this message:

> The complex type property 'EntityN.someValue' is configured as required (non-nullable) but has a null value when saving changes. Only non-null complex properties are supported by EF Core 8.
 
Here's the test:

```csharp
[Fact]
public async Task NonNullableSinglePrimitiveValuedValueObject_FailWhenNull()
{
    await using MyDbContext ctx = SetupContext();
    EntityN entity = new(Guid.NewGuid());
    await ctx.EntityNs.AddAsync(entity);
    
    Assert.Throws<InvalidOperationException>(() => ctx.SaveChanges());
}
```


### Sources
https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew#value-objects-using-complex-types
