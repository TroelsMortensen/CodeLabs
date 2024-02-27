# List of Value Objects

Sometimes, you need a list of value objects. Maybe you have multiple MiddleName(s), or multiple PhoneNumber(s).

This case covers that.

Now, if you remember you relational database theories, you know that you should not have a list in a column. You should have a separate table for the list.\
That's what we will get. It will actually look a bit like the nullable nested value objects.

Now, there's a difference between having a list of value objects, and having a list of entities.\
The entities are handled on a later slide.

The complex type approach cannot yet handle lists. So we go with the owned entity type approach.

It does not matter whether the properties on the value object are nullable or not.

### Value object
First, the usual value object:

```csharp
public class ValueObjectT
{
    public string Value { get; }

    public static ValueObjectT Create(string value) => new(value);

    private ValueObjectT(string value)
        => Value = value;

    private ValueObjectT()
    {
    }
}
```

### Entity
This time with a twist. We have a _list_ of value objects:

```csharp
public class EntityT
{
    public Guid Id { get; }
    internal List<ValueObjectT> someValues;
    
    public EntityT(Guid id)
    {
        Id = id;
        someValues = new();
    }
    
    public void AddValue(ValueObjectT v) => someValues.Add(v);
}
```

### Configuration
And then the configuration. Again, this will be done with the owned entity type approach, and we move the value objects to their own table, similar to the previous slide with nested value objects.

```csharp
private void ConfigureListValueObjects(EntityTypeBuilder<EntityT> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder.OwnsMany<ValueObjectT>("someValues", valueBuilder =>
    {
        valueBuilder.Property<int>("Id").ValueGeneratedOnAdd();
        valueBuilder.HasKey("Id");
        valueBuilder.Property(x => x.Value);
    });
}
```
As always, we define the primary key of the entity.

This time, because there are many value objects, we use the `OwnsMany` method.\
Again, we define the type, `ValueObjectT`, and the name of the navigation property, `someValues`, on the Entity.

Line 7: This configuration creates a new table, 'ValueObjectT', and this table needs a primary key.
We don't have an obvious candidate, so this line adds a new "[shadow property](https://learn.microsoft.com/en-us/ef/core/modeling/shadow-properties)", of type `int`, and we name it "Id".

Line 8: We then define this new property as the primary key.

Line 9: And then we define the property of the value object, `Value`.

If you have multi-valued value object, the mapping will just point to each of the properties.\
If you have nested value object, you will define that similar to the single nested value object case.

If we produce the sql script, we now get this:

```sql
CREATE TABLE "EntityTs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityTs" PRIMARY KEY
);

CREATE TABLE "ValueObjectT" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ValueObjectT" PRIMARY KEY AUTOINCREMENT,
    "Value" TEXT NOT NULL,
    "EntityTId" TEXT NOT NULL,
    CONSTRAINT "FK_ValueObjectT_EntityTs_EntityTId" FOREIGN KEY ("EntityTId") REFERENCES "EntityTs" ("Id") ON DELETE CASCADE
);
```

You may notice the ValueObjectT::Id property, which was added in the configuration. We also get a foreign key from ValueObjectT back to the owning entity, EntityT.

### Test
The test looks like follows:

```csharp
public async Task ListOfValueObjects()
{
    await using MyDbContext ctx = SetupContext();
    EntityT entity = new(Guid.NewGuid());
    ValueObjectT vo1 = ValueObjectT.Create("Hello world");
    ValueObjectT vo2 = ValueObjectT.Create("Hello world2");
    ValueObjectT vo3 = ValueObjectT.Create("Hello world3");
    
    entity.AddValue(vo1);
    entity.AddValue(vo2);
    entity.AddValue(vo3);
    
    await SaveAndClearAsync(entity, ctx);
    
    EntityT retrieved = ctx.EntityTs
        .Single(x => x.Id == entity.Id);
    
    Assert.NotEmpty(retrieved.someValues);
    Assert.Contains(retrieved.someValues, x => x.Value == vo1.Value);
    Assert.Contains(retrieved.someValues, x => x.Value == vo2.Value);
    Assert.Contains(retrieved.someValues, x => x.Value == vo3.Value);
}
```
* Create the DbContext
* Create the entity
* Create the value objects
* Add the value objects to the entity
* Save the entity, clear the change tracker
* Retrieve the entity
* Assert stuff about the list of values.

You will notice, I do not have to use Include("someValues"). This is because the owned entity type is always included when the owning entity is retrieved.\

### Sources
https://thehonestcoder.com/ddd-ef-core-8/