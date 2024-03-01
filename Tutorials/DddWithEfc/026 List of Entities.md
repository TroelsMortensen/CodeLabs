# List of Entities
Sometimes an entity has a list of other entities. The common example is Order having multiple OrderLines.

### Entities
We start with two entities

```csharp
public class EntityChildB
{
    public Guid Id { get; }

    public EntityChildB(Guid id)
        => Id = id;
}

public class EntityRootB
{
    public Guid Id { get; }

    internal List<EntityChildB> children = new();

    public EntityRootB(Guid id)
        => Id = id;
    
    public void AddChild(EntityChildB nestedEntity)
        => children.Add(nestedEntity);
}
```

First, the child entity, it's boring, just an Id.\
Then the root entity, or parent, which has a list of EntityChildB.

### Configuration
The end result will look very similar to the previous, but it is now a one-to-many, so the configuration is slightly different.

```csharp
private void ConfigureListOfNestedEntities(
    EntityTypeBuilder<EntityChildB> entityBuilderChild,
    EntityTypeBuilder<EntityRootB> entityBuilderRoot)
{
    entityBuilderChild.HasKey(x => x.Id);
    entityBuilderRoot.HasKey(x => x.Id);

    entityBuilderRoot
        .HasMany<EntityChildB>("children")
        .WithOne()
        .HasForeignKey("parentId")
        .OnDelete(DeleteBehavior.Cascade);
}
```

1) Define primary keys
2) We use the entity builder for the root.
3) We say the root "HasMany" children, of type EntityChildB. The field is called "children".
4) We say that the child has one parent.
5) We define the foreign key, "parentId", this will automatically go onto the child as a shadow property
6) And that it should cascade delete, so if the parent is deleted, all children are deleted.

### Test
We create a test with a root and several children, and then verify all children are loaded correctly.

```csharp

```