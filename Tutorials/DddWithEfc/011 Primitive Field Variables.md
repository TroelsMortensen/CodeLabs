# Primitive Field Variables
This case covers how to configure field variables, which are not public properties.\
I.e. something like this:

```csharp
private string myString;
internal int myInt;
```

Whether the field is private or internal, it is the same approach.

### Entity
First, we look at the entity:

```csharp
public class Entity0
{
    public Guid Id { get; }
    internal string myString;
    
    public Entity0(Guid id)
    {
        Id = id;
    }
    
    public void SetString(string newString)
        => myString = newString;
}
```

This entity has an Id of type Guid. And the field variable is of type string, called `myString`.\
There is a set method, to set the field.

### Configuration
EFC will auto-discover public properties, but not field variables.\
So we have to configure this manually.\
We must tell EFC that there is a field variable called "myString".

```csharp
private static void ConfigurePrivateFieldPrimitiveType(EntityTypeBuilder<Entity0> entityBuilder)
{
    entityBuilder
        .Property<string>("myString");
}
```
The EntityTypeBuilder class, i.e. the parameter, is a class used to configure a specific entity. In our case `Entity0`.
In the method, we use the `entityBuilder` to configure a _property_ of type `<string>`, called "myString".

This is enough, and EFC will now recognize this field, and can persist and load the entity and set the value of the field.