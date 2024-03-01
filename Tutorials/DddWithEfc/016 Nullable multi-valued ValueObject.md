# Nullable Multi-valued Value Object
This case covers how to configure a _nullable_ property value object, containing more than one value.

In this case the property/field on the entity can be null, or any property on the value object can be null.

### Value object
Here is the value object:

```csharp
public class ValueObjectQ
{
    public string? First { get; }
    public int? Second { get; }

    public static ValueObjectQ Create(string? first, int? second) 
        => new ValueObjectQ(first, second);
    
    private ValueObjectQ(string? first, int? second) 
        => (First, Second) = (first, second);
    
    private ValueObjectQ(){}
}
```

Notice now the types of the properties: `string?` and `int?`, meaning we allow either value to be null.\
You may also _not_ allow them to be null, or pick only one of them. But if you need any kind of nullability anywhere, this case covers that.

### Entity
Then the entity:

```csharp
public class EntityQ
{
    public Guid Id { get; }
    internal ValueObjectQ? someValue;

    public EntityQ(Guid id)
    {
        Id = id;
    }

    public void SetValue(ValueObjectQ v) => someValue = v;
}
```

Also here, the field is nullable: `ValueObjectQ?`. I am just making everything nullable for this example, but again, if you need any combination of nullable and not-nullable, then this case covers that.

### Configuration
We must use the owned entity type approach. We will still get the values flattened onto EntityQ, as you will see below. 

```csharp
private void ConfigureNullableMultiPrimitiveValuedValueObject(EntityTypeBuilder<EntityQ> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder.OwnsOne<ValueObjectQ>("someValue", ownedNavigationBuilder =>
    {
        ownedNavigationBuilder.Property(valueObject => valueObject.First)
            .HasColumnName("First");

        ownedNavigationBuilder.Property(valueObjectP => valueObjectP.Second)
            .HasColumnName("Second");
    });
}
```

This looks somewhat like the complex type approach, but we use `OwnsOne` instead of `ComplexProperty`.\
We say EntityQ "OwnsOne" of type ValueObjectQ, and the property is called "someValue".\
The lambda expression then configures the properties of ValueObjectQ. Again, the columns are renamed.

If we run the CLI command to generate the script, we see the following table:

```sql
CREATE TABLE "EntityQs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityQs" PRIMARY KEY,
    "First" TEXT NULL,
    "Second" INTEGER NULL
);
```

Notice how "someValue" is not part of the table. But the two properties of the value object is.

#### Note
If you have two value objects with properties of the same name, you should not rename the columns.\
This will probably cause a conflict in the database.\

### Test
The testing should cover various combinations of null and not-null values. I will provide:
* All _not_ null
* Property on entity is null
* One of the properties on the value is null

#### Non-null:
This test sets all values to non-null, and asserts that the values are retrieved correctly.

```csharp
[Fact]
public async Task MullableMultiValuedValueObject_NoneAreNull()
{
    await using MyDbContext ctx = SetupContext();
    EntityQ entity = new(Guid.NewGuid());
    ValueObjectQ valueObject = ValueObjectQ.Create("Hello world", 42);
    entity.SetValue(valueObject);
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityQ retrieved = ctx.EntityQs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(valueObject.First, retrieved.someValue.First);
    Assert.Equal(valueObject.Second, retrieved.someValue.Second);
}
```

#### Entity property is null:
This test sets the value object to null, and asserts that the value object is null when retrieved.

```csharp
[Fact]
public async Task NullableMultiValuedValueObject_EntityPropertyIsNull()
{
    await using MyDbContext ctx = SetupContext();
    EntityQ entity = new(Guid.NewGuid());
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityQ retrieved = ctx.EntityQs.Single(x => x.Id == entity.Id);
    Assert.Null(retrieved.someValue);
}
```

#### Value object, one property is null
This test sets the value object to have one null-property, 
and asserts that the value object is retrieved correctly.

```csharp
[Fact]
public async Task NullableMultiValuedValueObject_OneValueObjectPropertyIsNull()
{
    await using MyDbContext ctx = SetupContext();
    EntityQ entity = new(Guid.NewGuid());
    entity.SetValue(ValueObjectQ.Create("Hello world", null));
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityQ retrieved = ctx.EntityQs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Null(retrieved.someValue!.Second);
    Assert.Equal("Hello world", retrieved.someValue!.First);
}
```


### Sources:

https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
