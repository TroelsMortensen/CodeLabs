# Configuring Strongly Typed Primary Key

This case covers the use of strongly typed Ids for primary keys.

Per DDD your IDs should be strongly typed, so they are not confused with each other, and making the code clearer.

Again, my example is minimal, your own code is probably more elaborate.

### Strong Id
First, I show the class for my strong Id, for this example.

```csharp
public class MId
{
    public Guid Value { get; }

    public static MId Create() => new MId(Guid.NewGuid());

    public static MId FromGuid(Guid guid) => new MId(guid);

    private MId(Guid guid) => Value = guid;
    
    public override bool Equals(object? obj)
    {
        //...
    }
}
```

We have the get-only property.

Then the static factory method to create a new MId. 
I'm using the lambda expression here, called [Expression Body](https://www.geeksforgeeks.org/expression-bodied-members-in-c-sharp/) for more concise code. It's a personal preference.
You can also just do `{return new MId(Guid.NewGuid());}` instead of the `=>`.

You are also going to need a method, which can convert a Guid into an MId, so that's the `FromGuid()` method.

And then the private constructor.

Finally, you must be able to compare two MId instances. 
Maybe your MId inherits from a ValueObjectBase, or an IdBase (I found this useful), or you have just implemented the equals method.
Maybe you have overloaded `==` and `!=` too.

**Alternatively**, if you don't have the two static methods, two public constructors could probably also work:
1) Takes no parameters, creates new Guid, and sets it.
2) Takes a Guid, and sets it.

The idea is the same. We must be able to create a new MId, 
and we must be able to reconstruct one from a Guid.

Are you using Result? Should still be okay. Some code further down might change a bit.

### The Entity
Next up, the entity using this Id, i.e. EntityM. Here:

```csharp
public class EntityM
{
    public MId Id { get; }
    public EntityM(MId id) => Id = id;
    private EntityM(){}
}
```

We have the Id, as get-only property.\
A constructor to set it. Are you using factory method? Result? Doesn't matter, this can be reworked.\
A private constructor for EFC to use.

### The Configuration
This is a little more complicated. This time, I only show the configuration method. You still need the DbSet, and to call this method, obviously.

Here:

```csharp
private void ConfigureEntityM(EntityTypeBuilder<EntityM> entityBuilder)
{
    entityBuilder.HasKey(x => x.Id);

    entityBuilder
        .Property(m => m.Id)
        .HasConversion(
            mId => mId.Value,
            dbValue => MId.FromGuid(dbValue)
        );
}
```
Line 3: We say EntityM has a PK, and it is the Id property of EntityM.

But then, it is a strong type, which EFC cannot just save. 
What database type matches MId?\
So, we must define how to convert MId to something the database can understand. And how to convert back again to MId.

Line 6: We access the property `Id`, 
which gives us a `PropertyBuilder`, i.e. a class, 
which can configure specific properties, in this case `Id`.\
Line 7: We say that this property has a conversion, as mentioned just above. C# uses `MId`, the database uses `TEXT`. We must define how to convert back and to.
This is done with two lambda expressions:
* Line 8 is how to get the database value from `MId`, and here we just extract the Guid from the `MId`.
* Line 9 is how to convert db-value, i.e. `TEXT`, back to `MId`, and we do this from our static method, which takes a Guid, and wraps it into an `MId`.

Yes, but what about the Guid -> TEXT step? Luckily EFC can manage this for us, without explicit configuration.

You have a Result on `FromGuid`? I guess the line is then something like `MId.FromGuid(dbValue).Value`.


### The Test
The following test illustrates usage, and proves correctness:

```csharp
[Fact]
public async Task StrongIdAsPk()
{
    await using MyDbContext ctx = SetupContext();
    
    MId id = MId.Create();
    EntityM entity = new(id);
    
    await SaveAndClearAsync(entity, ctx);

    EntityM? retrieved = ctx.EntityMs.SingleOrDefault(x => x.Id.Equals(id));
    Assert.NotNull(retrieved);
}
```

1) Setup the DbContext and DB.
2) Create MId
3) Create entity
4) Save and clear
5) Retrieve entity
6) Assert that it exists
