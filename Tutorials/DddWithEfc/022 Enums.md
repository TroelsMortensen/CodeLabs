# Enums

Sometimes you need enums. EFC can handle this out of the box, so minimum configuration can be needed.\
However, when we use enums, it just results in an integer in the database.\
This is error prone, and it is recommended to use a string instead.\
This is because the integer value of an enum can change, and then the database will be inconsistent.\
The string value of an enum is more stable, and it is easier to understand when looking at the database.

This case covers the conversion from enum to string, and back.

### Entity

```csharp
public class Entity1
{
    public Guid Id { get; }

    internal MyEnum status = MyEnum.First;
    
    public Entity1(Guid id)
    {
        Id = id;
    }
    
    public void SetStatus(MyEnum newStatus) => status = newStatus;
}

public enum MyEnum
{
    First = 1,
    Second = 2
}
```

The entity has a field of type `MyEnum`, which is an enum defined below it.\
It is recommend to explicitly set the integer values of the enum, as shown here. This is because, you can then rearrange the order of enum values later, without breaking stuff. 
Or insert a new enum into the middle of the list, like this:

```csharp
public enum MyEnum
{
    First = 1,
    FirstAndAHalf = 3,
    Second = 2
}
```

### Configuration
The configuration:

```csharp
private void ConfigureEnumWithConversion(EntityTypeBuilder<Entity1> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);
    
    entityBuilder.Property<MyEnum>("status")
        .HasConversion(
            status => status.ToString(), 
            value => (MyEnum)Enum.Parse(typeof(MyEnum), value)
        );
}
```

Define PK.

Then access the property called "status", of type `MyEnum`.\
We define the conversion, from `MyEnum` to string, and back.\

### Test
Eh, some day..