# Single Nested Entity
Within aggregates, one root entity can have a composition to another entity.

This is a 1:1 relationship, where the root entity is the parent, and the nested entity is the child.

### Entities
We need two entities, one root and one nested.

```csharp
public class EntityChildA
{
    public Guid Id { get; }
    
    public EntityChildA(Guid id)
    {
        Id = id;
    }
}

public class EntityRootA
{
    public Guid Id { get; }

    internal EntityChildA child = null!;

    public EntityRootA(Guid id)
        => Id = id;

    private EntityRootA() // EFC
    {
    }

    public void SetChild(EntityChildA nestedEntity)
        => child = nestedEntity;
}
```

The first class, `EntityChildA`, is the nested entity. It has an Id, and a constructor.\
The second class, `EntityRootA`, is the root entity. It has an Id, and a field of type `EntityChildA`.\

This becomes a one to one relationship.

### Configuration
We must configure the child entity, there is not much interesting there, just defining the primary key.\
The interesting part is the root entity, where we define the composition.

Here's the configuration:

```csharp
private void ConfigureSingleNestedEntity(
    EntityTypeBuilder<EntityChildA> entityBuilderChild,
    EntityTypeBuilder<EntityRootA> entityBuilderRoot)
{
    entityBuilderChild.HasKey(x => x.Id);
    entityBuilderRoot.HasKey(x => x.Id);

    entityBuilderRoot
        .HasOne<EntityChildA>("child")
        .WithOne()
        .HasForeignKey<EntityChildA>("parentId")
        .OnDelete(DeleteBehavior.Cascade);
}
```
We define primary keys for both entities.\
Then we define the composition.\
Line 8: We use the entity builder for the EntityRootA.
Line 9: We say EntityRootA has one of EntityChildA, and the field is called "child".\
Line 10: Then we say that EntityChildA has one of EntityRootA.\
Line 11: We define the foreign key, which is a field on "EntityRootA". It does not exist in code, so this becomes a shadow property.\
Line 12: We also define that if the root entity is deleted, the child entity should also be deleted.

The script generated looks like this, now. 
Notice the foreign key on the child, "parentId", pointing to the parent.

```sql
CREATE TABLE "EntityRootAs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityRootAs" PRIMARY KEY
);


CREATE TABLE "EntityChildAs" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EntityChildAs" PRIMARY KEY,
    "parentId" TEXT NULL,
    CONSTRAINT "FK_EntityChildAs_EntityRootAs_parentId" FOREIGN KEY ("parentId") REFERENCES "EntityRootAs" ("Id") ON DELETE CASCADE
);
```

### Test
To prove that this works, we write a test.

```csharp
[Fact]
public async Task SingleNestedEntity()
{
    await using MyDbContext ctx = SetupContext();
    EntityRootA root = new(Guid.NewGuid());
    EntityChildA child = new(Guid.NewGuid());
    root.SetChild(child);

    await SaveAndClearAsync(root, ctx);

    EntityRootA retrievedRoot = ctx.EntityRootAs
        .Include("child")
        .Single(x => x.Id == root.Id);

    Assert.NotNull(retrievedRoot.child);
    Assert.Equal(child.Id, retrievedRoot.child.Id);
}
```
1) Create the DbContext
2) Create the root entity, parent
3) Create the nested entity, child
4) Set the nested entity on the root entity
5) Save the root entity, clear the change tracker. Notice the child is also saved, as EFC saves an entire object graph.
6) Retrieve the root entity, and include the child entity. Notice we have to explicitly include the child, by referencing the field variable by name. The child is not automatically loaded, because it is not configured as an _owned entity type_.
7) Assert that the child is not null
8) Assert that the Id of the child is the same as the Id of the child we created.