# Enumeration class

It is occasionally recommend to not use enum, but instead an enumeration class. This is because the integer value of an enum can change, and then the database will be inconsistent. 
The string value of an enumeration class is more stable, and it is easier to understand when looking at the database.

Because of persistence, it does require a more elaborate enumeration class.

It looks like this:

```csharp
public class MyStatusEnum
{
    public static MyStatusEnum First { get; } = new("First");
    public static MyStatusEnum Second { get; } = new("Second");
    public static MyStatusEnum Third { get; } = new("Third");
    
    private readonly string backingValue;
    private MyStatusEnum(string value)
        => backingValue = value;

    private MyStatusEnum() {}
    
    private bool Equals(MyStatusEnum other)
        => backingValue == other.backingValue;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MyStatusEnum)obj);
    }

    public override int GetHashCode()
        => backingValue.GetHashCode();
}
```

The first three properties are the enumaration values. They are static, and they are of the enumeration class type. They are initialized with a string value.

We have two constructors, both private, so you can only create instances of this class through the static properties.

Then there's some equality stuff making sure that the string value is used for comparison.

The entity using this enumeration class looks like this:

```csharp
public class EntityH
{
    public Guid Id { get; }
    internal MyStatusEnum status = MyStatusEnum.First;
    
    public EntityH(Guid id)
    {
        Id = id;
    }

    private EntityH() // EFC
    {
    }
}
```

The usual Id, and then a field of type MyStatusEnum.

You should probably add a mutator method for this status field.

### Configuration
We apply the complex property type approach. See slide 11.
The configuration looks like this:

```csharp
private void ConfigureEnumAsClass(EntityTypeBuilder<EntityH> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);
    entityBuilder.ComplexProperty<MyStatusEnum>("status",
        propBuilder =>
        {
            propBuilder.Property("backingValue")
                .HasColumnName("status");
        }
    );
}
```
Define PK.\
Then access the property called "status", of type `MyStatusEnum`. Say it is a complex type.\
We say that MyStatusEnum has a field variable called "backingValue", but in the database the column should just be called "status". 
Rename this as needed. It is to avoid the column name "status_backingValue", which is not very meaningful.

### Test

```csharp
[Fact]
public async Task ClassAsEnum()
{
    await using MyDbContext ctx = SetupContext();
    EntityH h = new(Guid.NewGuid());

    await SaveAndClearAsync(h, ctx);

    EntityH retrieved = ctx.EntityHs.Single(x => x.Id == h.Id);

    Assert.Equal(MyStatusEnum.First, retrieved.status);
}
```

Hmm, maybe a bit of a boring test, because "First" is the default value.\
You should probably add a mutator method, so you can change the status value, and actually do that in the test.

Man, I'm getting lazy and tired of writing this guide.