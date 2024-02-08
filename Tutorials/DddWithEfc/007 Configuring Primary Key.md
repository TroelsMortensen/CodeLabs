# Configuring Primary Keys

EFC uses convention to discover primary keys, so if you follow these, it's easy.\
But we shouldn't, when doing a DDD domain model.

You are either using a Guid as primary key, or you have a strong type, e.g. EventId, or GuestId.\
To be DDD compliant, I recommend the latter approach.\
I will show how each are configured below.


## Configuring Guid Primary Key
EFC can out of the box handle Guid types, they are generally just converted to text in the database.

You probably have a public property for the Guid, but with either no setter, or at least it is private.\
Like this:

```csharp
public Guid Id { get; }
```

You should not have a `private set;` included, as the Id should never be changed after the entity has been instantiated.
By leaving out a setter, the Id can only be set upon construction, i.e. from the constructor.

This here is the generic looking entity, we will configure. It is minimal. You may have a factory method, that's irrelevant.

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

Here is the configuration method for EntityL (just a generic name, I have Entities A to L, at the time of writing):

```csharp
...
public DbSet<EntityL> EntityLs => Set<EntityL>();
...
    

```