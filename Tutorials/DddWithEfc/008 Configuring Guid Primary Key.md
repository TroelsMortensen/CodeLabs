# Configuring Guid Primary Key

EFC uses convention to discover primary keys, so if you follow these, it's easy.\
But we don't, and shouldn't, when doing a DDD domain model.

You are either using a Guid as primary key, or you have a strong type, e.g. `EventId`, or `GuestId`.\
To be DDD compliant, I recommend the latter approach.\
First, we deal with the Guid. Next slide is the strongly typed Id.


## The Code
EFC can out of the box handle Guid types, they are generally just converted to text in the database.

You probably have a public property for the Guid, but with either no setter, or at least it is private.\
Like this:

```csharp
public Guid Id { get; }
```

You should not have a `private set;` included, as the Id should never be changed after the entity has been instantiated.
By leaving out a setter, the Id can only be set upon construction, i.e. from the constructor.

This here is the generic looking entity, we will configure. It is minimal, stripped bare. You may have a factory method, that's irrelevant.

```chsparp
public class EntityL
{
    public Guid Id { get; }

    public EntityL(Guid id)
    {
        Id = id;
    }
    
    ...
}
```
We have the Guid Id property, to be used as primary key.\
There is a constructor to set it. You may have a factory method, either works.

EFC cannot automatically discover this property, because there is no setter. If there was a private setter, it would work. 
But we don't need that, and won't do that. We will aim to minimize the effect EFC has on our domain model.\
Therefore, we have to explicitly tell EFC, that we have a property called Id, and it should be used as primary key.

Here is the configuration method for EntityL (just a generic name, I have Entities A to L, at the time of writing this):

```csharp
// ... other sets
public DbSet<EntityL> EntityLs => Set<EntityL>();

// ... other methods

protected override void OnModelCreating(ModelBuilder mBuilder)
{
    ConfigureEntityL(mBuilder.Entity<EntityL>());
}

private void ConfigureEntityL(EntityTypeBuilder<EntityL> entityBuilder)
{
    entityBuilder.HasKey(entity => entity.Id);
}
```

We have the DbSet defined.\
The `OnModelCreating` calls our configure method with an argument `mBuilder.Entity<EntityL>()`.\
This argument gives ss an EntityTypeBuilder, which is a class used to configure a specific entity.

In our configure method, we just call `HasKey`, with a lambda expression pointing to the specific Id property.\
This means, we are creating a configuration for the entity of type `EntityL`, saying it has a primary key, 
and that pk is the `Id` property on the class.

### Guid Pk Test
The following test shows that we can:
1) Create an entity with a Guid value
2) Save the entity
3) Retrieve it again

```csharp
[Fact]
public async Task GuidAsPk()
{
    await using MyDbContext ctx = SetupContext();
    Guid id = Guid.NewGuid();
    EntityL entity = new(id);
    
    await SaveAndClearAsync(entity, ctx);

    EntityL? retrieved = ctx.EntityLs.SingleOrDefault(x => x.Id == id);
    Assert.NotNull(retrieved);
}
```