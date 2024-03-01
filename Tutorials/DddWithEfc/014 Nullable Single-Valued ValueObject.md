# Nullable single-valued value object

This case covers how to configure a _nullable_ property value object, containing a single value.

Now, the "complex type" feature used on the previous slide does not support nullability. Hopefully in EFC9.

Instead, we have to say the value object is an entity, 
and configure it like that. It looks pretty similar. But feels like a nasty hack, because the value object will actually become an entity, with an id, in the database.

### Value Object
First, the value object. Basically the same as the previous case.\
I'm just keeping cases cleanly separated, so I'm using a new letter suffix.

```csharp
public class ValueObjectO
{
    public string Value { get; }

    public static ValueObjectO Create(string input) => new ValueObjectO(input);
    private ValueObjectO(string input) => Value = input;

    private ValueObjectO()
    {
    }
}
```
Notice, the `Value` here is non-nullable. It's in the entity, the nullable part happens.

### Entity

Here's the entity, notice the field, it is marked with `?` to indicate the reference may be null: 

```csharp
public class EntityO
{
    public Guid Id { get; }

    internal ValueObjectO? someValue;
    
    public EntityO(Guid id)
    {
        Id = id;
    }

    public void SetValue(ValueObjectO v) => someValue = v;
}
```

### Configuration
As mentioned above, we need to take a different approach to configure this case.

Here we go:

```csharp
private void ConfigureNullableSinglePrimitiveValuedValueObject(EntityTypeBuilder<EntityO> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder
        .OwnsOne<ValueObjectO>("someValue")
        .Property(vo => vo.Value);
        .HasColumnName("value");
}
```

The first line of code in the body, as usual, configures the ID of EntityO.

Then...\
Line 5: Use the entity type builder for EntityO.\
Line 6: Say that EntityO _owns a single other entity_, of type ValueObjectO, and the property (field) on EntityO is called "someValue".
This basically says ValueObjectO should be an entity in itself. When it is a single value, it can still be flattened onto the EntityO.\
If we look at the script generated, the table for EntityO is defined like this:

```sql
CREATE TABLE "EntityOs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityOs" PRIMARY KEY,
    "someValue_Value" TEXT NULL
);
```
Line 7: Here we extract the `Value` of the value object.\
Line 8: Notice that the table gets a column named by combining the property/field name, with the property name on the value object: "someValue_Value".
The last line here is not strictly necessary, but we may want to rename the column to something more meaningful.

Again, the value object becomes a part of the entity, i.e. the same table.

### Test
The first test shows, that we can add the value object to the entity, save and retrieve it again.

```csharp
[Fact]
public async Task NullableSinglePrimitiveValuedValueObject()
{
    await using MyDbContext ctx = SetupContext();
    ValueObjectO value = ValueObjectO.Create("Hello world");
    EntityO entity = new(Guid.NewGuid());
    entity.SetValue(value);

    await SaveAndClearAsync(entity, ctx);

    EntityO retrieved = ctx.EntityOs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(value.Value, retrieved.someValue.Value);
}
```

The second test shows, that we can save an entity, without a value object.

```csharp
[Fact]
public async Task NullableSinglePrimitiveValuedValueObject_SaveWhenNulled()
{
    await using MyDbContext ctx = SetupContext();
    EntityO entity = new(Guid.NewGuid());

    await SaveAndClearAsync(entity, ctx);

    EntityO retrieved = ctx.EntityOs.Single(x => x.Id == entity.Id);
    Assert.Null(retrieved.someValue);
}
```

### Sources:

https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
