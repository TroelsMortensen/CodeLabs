# Strongly Typed Foreign Key

This case covers a foreign key, where the primary key is a strongly typed id, i.e. value object.

It will again be a one-to-many relationship, where EntityX has a foreign key to EntityY.

## Entities
We are going with EntityX and Y. EntityY has the following strong ID:

```csharp
public class YId
{
    public Guid Value { get; }

    public static YId Create()
        => new(Guid.NewGuid());

    public static YId FromGuid(Guid guid)
        => new(guid);

    private YId(Guid guid)
        => Value = guid;
}
```

And then EntityY:

```csharp
public class EntityY
{
    public YId Id { get; }

    public EntityY(YId id) => Id = id;
}
```

And finally EntityX:

```csharp
public class EntityX
{
    public Guid Id { get; }

    internal YId foreignKeyToY;

    public EntityX(Guid id)
    {
        Id = id;
    }

    public void SetFk(YId id) => foreignKeyToY = id;
}
```

It is irrelevant for the example whether EntityX has a strong ID or not, so I've used the simpler approach.

### Configuration
Again we need referential integrity on the foreign key, so that is configured as well.

```csharp
private void ConfigureStronglyTypedFk(EntityTypeBuilder<EntityX> entityBuilderX, EntityTypeBuilder<EntityY> entityBuilderY)
{
    entityBuilderX.HasKey(x => x.Id);

    entityBuilderY.HasKey(y => y.Id);
    
    entityBuilderY.Property(y => y.Id)
        .HasConversion(
            yId => yId.Value,
            dbValue => YId.FromGuid(dbValue)
        );

    entityBuilderX.HasOne<EntityY>()
        .WithMany()
        .HasForeignKey("foreignKeyToY");
}
```

First, we define the PK on EntityX. That's just a Guid, so that's simple.\
The we define the PK on EntityY. That is a strongly typed ID, so we also have to configure the conversion between YId and Guid. This was introduced on slide 9.\
And finally, we define that EntityX HasOne EntityY, which has many (WithMany) EntityY.\
The last line defines the property on EntityX, which should act as a foreign key.

### Test
Again we create two tests, one to show this works, and one to show that the referential integrity constraint is in place.

```csharp
[Fact]
public async Task StrongIdAsFk_ValidTarget()
{
    await using MyDbContext ctx = SetupContext();
    EntityY entityY = new (YId.Create());
    await SaveAndClearAsync(entityY, ctx);

    EntityX entityX = new(Guid.NewGuid());
    entityX.SetFk(entityY.Id);

    await SaveAndClearAsync(entityX, ctx);

    EntityX retrievedX = ctx.EntityXs.Single(x => x.Id == entityX.Id);

    EntityY? retrievedY = ctx.EntityYs
        .SingleOrDefault(y => y.Id == retrievedX.foreignKeyToY);

    Assert.NotNull(retrievedY);
}
```

First 3 lines, we add a new EntityY to the database.

Then we create an EntityX, and set the foreign key to point to the EntityY, we just created.\
EntityX is now saved to the database, without problems.

We retrieve EntityX, and use its foreign key to retrieve EntityY.

Then the test to verify the referential integrity:

```csharp
[Fact]
public async Task StrongIdAsFk_InvalidTarget()
{
    await using MyDbContext ctx = SetupContext();
    YId yId = YId.Create();
    EntityX entityX = new(Guid.NewGuid());
    entityX.SetFk(yId);

    ctx.EntityXs.Add(entityX);

    Action exp = () => ctx.SaveChanges();

    Exception? exception = Record.Exception(exp);
    Assert.NotNull(exception);
}
```

A "dummy" YId is created, but not associated with any EntityY.\
Then EntityX, which gets this YId, pointing to nothing.

When we save the changes, we get an exception back.