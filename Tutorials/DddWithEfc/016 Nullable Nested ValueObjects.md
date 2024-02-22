# Nullable Nested Value Objects

Again, because of nullability, we cannot use the complex type approach.

We use the owned entity type approach. This time, it will result in a separate table, as you will see.

### Value objects

First, the top level value object. They will all look similar to the previous slide, but with nullable properties.

```csharp
public class ValueObjectS
{
    public NestedValueObjectS1? First { get; }
    public NestedValueObjectS2? Second { get; }

    public static ValueObjectS Create(NestedValueObjectS1? first, NestedValueObjectS2? second)
        => new ValueObjectS(first, second);

    private ValueObjectS(NestedValueObjectS1? first, NestedValueObjectS2? second)
        => (First, Second) = (first, second);

    private ValueObjectS()
    {
    }
}
```

Notice both properties are nullable.

Next, the nested value objects. Also with nullable properties:

```csharp
public class NestedValueObjectS1
{
    public string? Value { get; }

    public static NestedValueObjectS1 Create(string? input)
        => new NestedValueObjectS1(input);

    private NestedValueObjectS1(string? input)
        => Value = input;

    private NestedValueObjectS1()
    {
    }
}

public class NestedValueObjectS2
{
    public int? Value { get; }

    public static NestedValueObjectS2 Create(int? input)
        => new NestedValueObjectS2(input);

    private NestedValueObjectS2(int? input)
        => Value = input;

    private NestedValueObjectS2()
    {
    }
}
```

### Entity

Here a very familiar looking entity.

```csharp
public class EntityS
{
    public Guid Id { get; }

    internal ValueObjectS? someValue;

    public EntityS(Guid id)
        => Id = id;
    
    public void SetValue(ValueObjectS v) => someValue = v;
    
    private EntityS()
    {
    }
}
```



### Configuration
And for the configuration. We use the owned entity type approach. And because of the nullability, we have to specify a separate table.\
There is a good reason for this:

Assume that we get the same flattened table: EntityS(Id, First, Second).\
What if both First and Second are null?\
Do we still have a non-null ValueObjectS? Just with null in both properties?\
Or do we not have a ValueObjectS at all?\
No-one knows\
But if we move the properties to a separate table: ValueObjectS(First, Second, EntitySId), 
we can see that ValueObjectS is _null_ in EntityS, if there is no row in ValueObjectS-table, 
and it is _not null_ if there is a row, even if both First and Second are null. Then EntitySId is not null.\
Confused?\
It's not super important. Just remember to use the owned entity type approach in case of any nullability, and you will be fine.

Now, if ValueObjectS is non-nullable, even though the nested values are, we might not need a separate table. 

If we do the separate table, we are safe in all cases, even though we may loose a tiny bit of performance.

And so, bla bla, here's the configuration. Things are getting complicated.

```csharp
private void ConfigureNullableNestedValueObjects(EntityTypeBuilder<EntityS> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);
    
    entityBuilder.OwnsOne<ValueObjectS>("someValue", ownedNavigationBuilder =>
    {
        ownedNavigationBuilder.ToTable("ValueObjectS");
        
        ownedNavigationBuilder.OwnsOne<NestedValueObjectS1>("First", fvo =>
        {
            fvo.Property(x => x.Value)
                .HasColumnName("First");
        });
        ownedNavigationBuilder.OwnsOne<NestedValueObjectS2>("Second", svo =>
        {
            svo.Property(x => x.Value)
                .HasColumnName("Second");
        });
    });

    entityBuilder.Navigation("someValue");
}
```

Line 5: We use `OwnsOne` to define the nested value object, it is of type "<ValueObjectS>", and the field on EntityS is called "someValue".
Then we dive in to configure the value object hierarchy.

Line 7: We use `ToTable` to say the ValueObjectS should go to its own table, and specify the name of the table.\
We shouldn't need this line, if ValueObjectS is non-nullable on EntityS.

Line 9: We say ValueObjectS has a nested value object, of type "<NestedValueObjectS1>", and the field on ValueObjectS is called "First".\
We specify the property of NestedValueObjectS1, and rename the column to "First".

Line 14: We do the same for the second nested value object.

Line 21: We have to add a navigation property, because we have a separate table for ValueObjectS.\
This is used by EFC, so when we retrieve an EntityS, it knows how to get the related ValueObjectS.

We get the following SQL:

```sql
CREATE TABLE "EntitySs"
(
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntitySs" PRIMARY KEY
);

CREATE TABLE "ValueObjectS"
(
    "EntitySId" TEXT NOT NULL CONSTRAINT "PK_ValueObjectS" PRIMARY KEY,
    "First"     TEXT NULL,
    "Second"    INTEGER NULL,
    CONSTRAINT "FK_ValueObjectS_EntitySs_EntitySId" FOREIGN KEY ("EntitySId") REFERENCES "EntitySs" ("Id") ON DELETE CASCADE
);
```

Now, EntityS contains no extra attributes, they are moved to the second table. There, we find First and Second. Along with a foreign key to EntityS.\
Notice this foreign key is also a primary key. This is because of the one-to-one relationship between EntityS and ValueObjectS.\
There are corner cases, where this causes a problem. I will not go into that here. You shouldn't encounter those, I hope. 

// TODO go into that here?


### Test
Again, we should create various tests for combinations of null and not null values.\
First, all non-null.

```csharp
[Fact]
public async Task NullableNestedValueObject()
{
    await using MyDbContext ctx = SetupContext();
    EntityS entity = new(Guid.NewGuid());
    NestedValueObjectS1 nested1 = NestedValueObjectS1.Create("Hello world");
    NestedValueObjectS2 nested2 = NestedValueObjectS2.Create(42);
    ValueObjectS valueObject = ValueObjectS.Create(nested1, nested2);
    
    entity.SetValue(valueObject);
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityS retrieved = ctx.EntitySs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(valueObject.First!.Value, retrieved.someValue.First!.Value);
    Assert.Equal(valueObject.Second!.Value, retrieved.someValue.Second!.Value);
}
```
Then null property on EntityS:
```csharp
[Fact]
public async Task NullableNestedValueObject_NullProp()
{
    await using MyDbContext ctx = SetupContext();
    EntityS entity = new(Guid.NewGuid());
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityS retrieved = ctx.EntitySs.Single(x => x.Id == entity.Id);
    Assert.Null(retrieved.someValue);
}
```

Then one of the properties on ValueObjectS is null. 
```csharp{6}
[Fact]
public async Task NullableNestedValueObject_OneNestedPropIsNull()
{
    await using MyDbContext ctx = SetupContext();
    EntityS entity = new(Guid.NewGuid());
    NestedValueObjectS1 nested1 = NestedValueObjectS1.Create("Hello world");
    NestedValueObjectS2 nested2 = null; 
    ValueObjectS valueObject = ValueObjectS.Create(nested1, nested2);
    
    entity.SetValue(valueObject);
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityS retrieved = ctx.EntitySs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(valueObject.First!.Value, retrieved.someValue.First!.Value);
    Assert.Null(retrieved.someValue.Second);
}
```

And then a value on a nested value object is null:
```csharp{6}
[Fact]
public async Task NullableNestedValueObject_OnePropertyOnNestedValueIsNull()
{
    await using MyDbContext ctx = SetupContext();
    EntityS entity = new(Guid.NewGuid());
    NestedValueObjectS1 nested1 = NestedValueObjectS1.Create("Hello world");
    NestedValueObjectS2 nested2 = NestedValueObjectS2.Create(null); 
    ValueObjectS valueObject = ValueObjectS.Create(nested1, nested2);
    
    entity.SetValue(valueObject);
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityS retrieved = ctx.EntitySs.Single(x => x.Id == entity.Id);
    Assert.NotNull(retrieved.someValue);
    Assert.Equal(valueObject.First!.Value, retrieved.someValue.First!.Value);
    Assert.Null(retrieved.someValue.Second);
}
```

And so on. You get the idea. You can do the rest of the permutations yourself.