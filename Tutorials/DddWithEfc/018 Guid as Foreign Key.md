# Guid as Foreign Key

This case is about one entity having a foreign key to another entity, where the primary key is a Guid.

There is no value object this time around, instead we have two entities, which you can consider two aggregates.

EntityU will have a foreign key to EntityV.

It becomes a *:1 relationship, one-to-many.

EntityU *-----1 EntityV

### Entities

They will look familiar.

```csharp
public class EntityU
{
    public Guid Id { get; }

    private Guid entityVId;
    
    public EntityU(Guid id)
    {
        Id = id;
    }
    
    public void SetEntityVId(Guid id) => entityVId = id;
}

public class EntityV
{
    public Guid Id { get; }

    public EntityV(Guid id)
    {
        Id = id;
    }
}
```

You will notice that EntityU has a private field, `entityVId`. This is the foreign key, it reference some instance of EntityV. That's the intension.

Now, we _could_ decide to not care at all about foreign key constraints in the database, or [referential integrity](https://intelligent-ds.com/blog/what-is-referential-integrity).\
That EntityU cannot point to a non-existing EntityV could be handled in the domain.

However, because of later topics, we will need this foreign key constraint. So we will configure it.

### Configuration
Here:

```csharp
private void ConfigureGuidAsFk(EntityTypeBuilder<EntityU> entityUBuilder, EntityTypeBuilder<EntityV> entityVBuilder)
{
    entityUBuilder.HasKey(x => x.Id);
    entityVBuilder.HasKey(x => x.Id);

    entityUBuilder.Property<Guid>("entityVId");
    
    entityUBuilder.HasOne<EntityV>()
        .WithMany()
        .HasForeignKey("entityVId");
}
```

We have two entity-builders this time, one for each entity.\
We configure the primary key for each entity.\
We then tell EFC about the property `entityVId` on EntityU, it is of type `Guid`.\
Finally we configure the relationship.\
We say that EntityU has one EntityV, and that EntityV has many EntityU.\
We also say that the foreign key is `entityVId`.

### Test
We write two tests, a sunny and a rainy.

First, to prove that we can create a foreign key:

```csharp
[Fact]
public async Task GuidAsFk_ValidTarget()
{
    await using MyDbContext ctx = SetupContext();
    EntityV entityV = new(Guid.NewGuid());

    await SaveAndClearAsync(entityV, ctx);

    EntityU entityU = new(Guid.NewGuid());
    entityU.SetEntityVId(entityV.Id);

    await SaveAndClearAsync(entityU, ctx);

    EntityU retrievedU = ctx.EntityUs
        .Single(x => x.Id == entityU.Id);

    EntityV? retrievedV = ctx.EntityVs
                        .SingleOrDefault(x => x.Id == retrievedU.entityVId);
    Assert.NotNull(retrievedV);
}
```
First, we create an instance of EntityV, and save it.\
Then we create an instance of EntityU, and set the foreign key to the Id of the EntityV.\

This should work, as EntityU is now referencing an existing EntityV, which is already in the database.

We then retrieve the EntityU, and check that the foreign key points to an existing EntityV, which is then also retrieved.

Next up, the following test will show that an exception is thrown from the database, if we try to add a foreign key to a non-existing EntityV:

```csharp
[Fact]
public async Task GuidAsFk_InValidTarget()
{
    await using MyDbContext ctx = SetupContext();
    EntityU entityU = new(Guid.NewGuid());
    entityU.SetEntityVId(Guid.NewGuid());
    
    ctx.EntityUs.Add(entityU);
    Action exp = () => ctx.SaveChanges();

    Exception? exception = Record.Exception(exp);

    Assert.NotNull(exception);
}
```
Create an instance of EntityU, and set the foreign key to a new Guid. This Guid points to nothing.\
Then we add the EntityU to the context, and try to save changes.\
The saving will fail with an exception, which we record and assert is not null.

This is the exception message:

> SQLite Error 19: 'FOREIGN KEY constraint failed'

