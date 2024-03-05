# List of Strongly Typed Foreign Keys

Sometimes an entity needs to reference multiple instances of another type of entity.
This is a one-to-many relationship, but we change where the foreign key is. Now, we keep a list of foreign keys on the "parent"
entity, instead of a single foreign key on the "child" entity.

This is a common pattern, and it is often used when we have a "parent" entity, which has a list of "child" entities.

This time, instead of Guids, we will use strongly typed Ids.

### Entities
We start with EntityD:

```csharp
public class EntityD
{
    public DId Id { get; }

    public EntityD(DId id) => Id = id;
    
    private EntityD(){}
}

public class DId
{
    public Guid Value { get; }

    public static DId Create() => new(Guid.NewGuid());

    public static DId FromGuid(Guid id) => new(id);

    private DId(Guid newGuid)
        => Value = newGuid;
}
```

EntityD is as usual, now with a strongly typed Id. That class is defined above as well.\


Then we have EntityC:

```csharp
public class EntityC
{
    public Guid Id { get; }

    private string someValue = "42";

    internal List<DId> foreignKeysToD;

    public EntityC(Guid id)
    {
        Id = id;
        foreignKeysToD = new();
    }

    public void AddFk(DId id) => foreignKeysToD.Add(id);
}
```
Notice the list of DId instances, to reference multiple EntityDs.

Now, we need a similar trick as with the previous slide. Even though we have a list of strongly typed Ids, these have a defined mapping, which converts them to Guids, so we are back to the previous case. We cannot use DId as a wrapper class, with its own table.\
We must create a wrapper class for DId:

```csharp
public class ReferenceFromCtoD
{
    public DId FkToD { get; set; }
    public static implicit operator ReferenceFromCtoD(DId fk) => new(fk);
    public static implicit operator DId(ReferenceFromCtoD reference) => reference.FkToD;
    private ReferenceFromCtoD(DId fk) => FkToD = fk;
    private ReferenceFromCtoD(){}
}
```

This time two explicit constructors are apparently needed. Not sure why, compared to previous slide.\
And implicit operators for ease of use.

We update EntityC to use this wrapper class:

```csharp{5}
public class EntityC
{
    public Guid Id { get; }

    internal List<ReferenceFromCtoD> foreignKeysToD;

    public EntityC(Guid id)
    {
        Id = id;
        foreignKeysToD = new();
    }

    public void AddFk(DId id) => foreignKeysToD.Add(id);
}
```
And again, we don't have to update the `AddFk()` method because of the implicit operators.

Now we can configure this setup.

**First, re-run your _unit tests_. You have made implementation changes, which may affect your unit tests, or the internal logic in your aggregate. I had about 6 failed unit tests, because I was now comparing the wrapper class to the strong Id class.**



### Configuration
Here's the configuration code, the complexity is growing:

```csharp
private void ConfigureListOfStronglyTypedForeignKeys(ModelBuilder mBuilder)
{
    // First Ids on both
    mBuilder.Entity<EntityC>().HasKey(x => x.Id);
    mBuilder.Entity<EntityD>().HasKey(x => x.Id);


    // Then the conversion from strong ID to simple type
    mBuilder.Entity<EntityD>() // here we define the conversion for the ID
        .Property(m => m.Id)
        .HasConversion(
            id => id.Value, // how to convert ID type to simple value, EFC can understand
            value => DId.FromGuid(value)); // how to convert simple EFC value to strong ID.

    // Now we configure the join table
    mBuilder.Entity<ReferenceFromCtoD>(x =>
    {
        x.Property<Guid>("FkBackToC");
        x.HasKey("FkBackToC", "FkToD");
        x.HasOne<EntityC>()
            .WithMany("foreignKeysToD")
            .HasForeignKey("FkBackToC");

        x.Property(m => m.FkToD)
            .HasConversion(
                id => id.Value, // how to convert ID type to simple value, EFC can understand
                value => DId.FromGuid(value)); // how to convert simple EFC value to strong ID.

        x.HasOne<EntityD>()
            .WithMany()
            .HasForeignKey(y => y.FkToD);
    });
}
```

I will take out various parts of this configuration and explain them.

```csharp
mBuilder.Entity<EntityC>().HasKey(x => x.Id);
mBuilder.Entity<EntityD>().HasKey(x => x.Id);
```
Define the primary keys for both entities.

```csharp
mBuilder.Entity<EntityD>() 
        .Property(m => m.Id)
        .HasConversion(
            id => id.Value, 
            value => DId.FromGuid(value));
```
This is to define the conversion from DId to Guid, and back. It's the same as slide 9.\
Line 1: Access the EntityTypeBuilder for EntityD.\
Line 2: Configure the property `Id`.\
Line 3: Define the conversion.\
Line 4: How to convert from DId to Guid.\
Line 5: How to convert from Guid to DId.


Then we need the join table:
```csharp
mBuilder.Entity<ReferenceFromCtoD>(x =>
{
    x.Property<Guid>("FkBackToC");
    x.HasKey("FkBackToC", "FkToD");
    x.HasOne<EntityC>()
        .WithMany("foreignKeysToD")
        .HasForeignKey("FkBackToC");

    x.Property(m => m.FkToD)
        .HasConversion(
            id => id.Value, // how to convert ID type to simple value, EFC can understand
            value => DId.FromGuid(value)); // how to convert simple EFC value to strong ID.

    x.HasOne<EntityD>()
        .WithMany()
        .HasForeignKey(y => y.FkToD);
});
```

Line 1: Access the EntityTypeBuilder for ReferenceFromCtoD.\
Line 3: Define the property `FkBackToC`. This is the foreign key back to EntityC. It is not initially on the class `ReferenceFromCtoD`, so it is added as a shadow property.\
Line 4: Define the primary key for this join table. It is a composite key.\
Line 5: We say ReferenceFromCtoD has a reference to EntityC.\
Line 6: We say EntityC has many ReferenceFromCtoD. That's the list in EntityC.\
Line 7: We define the foreign key, which points from `ReferenceFromCtoD` back to `EntityC`.\
Line 9: Define the property `FkToD`. This is the foreign key to EntityD.\
Line 10: Define the conversion from DId to Guid, and back. This was actually also done above.
There is a way to define a conversion once, so you don't have to duplicate it. At some point I'll look into this.\
Line 14: We say ReferenceFromCtoD has a reference to EntityD.\
Line 15: We say EntityD has many ReferenceFromCtoD. This isn't present in the code, only the database.\
Line 16: We define the foreign key, which points from `ReferenceFromCtoD` to `EntityD`.

Again, because of non-nullability, we should get that on-delete cascade behaviour.\
You can probably override as needed, see the previous slide.

We get the following sql script:

```sql
CREATE TABLE "EntityCs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityCs" PRIMARY KEY
);

CREATE TABLE "EntityDs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityDs" PRIMARY KEY
);

CREATE TABLE "ReferenceFromCtoD" (
    "FkToD" TEXT NOT NULL,
    "FkBackToC" TEXT NOT NULL,
    CONSTRAINT "PK_ReferenceFromCtoD" PRIMARY KEY ("FkBackToC", "FkToD"),
    CONSTRAINT "FK_ReferenceFromCtoD_EntityCs_FkBackToC" FOREIGN KEY ("FkBackToC") REFERENCES "EntityCs" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ReferenceFromCtoD_EntityDs_FkToD" FOREIGN KEY ("FkToD") REFERENCES "EntityDs" ("Id") ON DELETE CASCADE
);

```

The join table is there, with the two foreign keys, and the composite primary key.\
And we get the `ON DELETE CASCADE` for both foreign keys. Again, this can be overwritten, you may not want cascade delete on the `FkToD` attribute.

### Test
First success case:

```csharp
public async Task ListOfStrongIdFkReferences()
{
    await using MyDbContext ctx = SetupContext();

    EntityD d1 = new (DId.Create());
    EntityD d2 = new (DId.Create());
    EntityD d3 = new (DId.Create());

    await ctx.EntityDs.AddRangeAsync(d1, d2, d3);
    await ctx.SaveChangesAsync();
    ctx.ChangeTracker.Clear();

    EntityC c = new EntityC(Guid.NewGuid());
    c.AddFk(d1.Id);
    c.AddFk(d2.Id);
    c.AddFk(d3.Id);

    await SaveAndClearAsync(c, ctx);

    EntityC retrieved = ctx.EntityCs
        .Include("foreignKeysToD")
        .Single(x => x.Id == c.Id);

    Assert.NotEmpty(retrieved.foreignKeysToD);
    Assert.Contains(retrieved.foreignKeysToD, x => x.FkToD.Value == d1.Id.Value);
    Assert.Contains(retrieved.foreignKeysToD, x => x.FkToD.Value == d2.Id.Value);
    Assert.Contains(retrieved.foreignKeysToD, x => x.FkToD.Value == d3.Id.Value);
}
```

* Create the DbContext.
* Create the "D" entities.
* Add to database and clear cache.
* Create the "C" entity, and add the foreign keys.
* Save the "C" entity, and clear the cache.
* Retrieve the "C" entity, notice again the `Include` statement. 
* Assert stuff about the list of foreign keys.

Then the failure case:

```csharp
[Fact]
public async Task ListOfStrongIdFkReferences_FailWithInvalidFk()
{
    await using MyDbContext ctx = SetupContext();
    EntityC c = new (Guid.NewGuid());
    c.AddFk(DId.Create());

    ctx.EntityCs.Add(c);

    Action exp = () => ctx.SaveChanges();
    Exception? exception = Record.Exception(exp);

    Assert.NotNull(exception);
}
```

Bla bla, assert that we get an exception, when trying to save EntityC, which references a non-existing EntityD.

## TODO 
This can be done as an owned entity, rather than explicitly making the wrapper class an entity. 
I feel this makes more sense, conceptually. Eventually, I will probably update this example.

Though, **the end result is the same**, and so it is not a priority.

For now, a quick and dirty solution is just pasting my configuration code, without explanation.

```csharp
private static void ParticipantsListConfiguration(EntityTypeBuilder<VeaEvent> entityBuilder)
{
    entityBuilder.OwnsMany<VeaEvent.GuestFk>("participants", valueBuilder =>
    {
        valueBuilder.Property(guestFk => guestFk.GuestId)
            .HasConversion(
                guestId => guestId.Get,
                dbValue => GuestId.FromGuid(dbValue).Payload
            )
            .HasColumnName("GuestFk");
        
        valueBuilder.Property(x => x.EventId)
            .HasColumnName("EventFk");
        
        valueBuilder.WithOwner()
            .HasForeignKey(x => x.EventId);
        
        valueBuilder.HasOne<Guest>()
            .WithMany()
            .HasForeignKey(fk => fk.GuestId);
        
        valueBuilder.HasKey(x => new {x.GuestId, x.EventId});
    });
}
```

I do get this script:

```sql
CREATE TABLE "GuestFk" (
    "GuestFk" TEXT NOT NULL,
    "EventFk" TEXT NOT NULL,
    CONSTRAINT "PK_GuestFk" PRIMARY KEY ("GuestFk", "EventFk"),
    CONSTRAINT "FK_GuestFk_Events_EventFk" FOREIGN KEY ("EventFk") REFERENCES "Events"
 ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GuestFk_Guests_GuestFk" FOREIGN KEY ("GuestFk") REFERENCES "Guests"
 ("Id") ON DELETE CASCADE
);
```
