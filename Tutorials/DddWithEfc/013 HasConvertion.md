# Has Conversion

I think this is the simplest approach to mapping most of your value objects, if they consist of a single value.

You will in the following slides see other approaches for corner cases, but just look at those, if this approach here does not work.

The HasConversion can handle both null and required values, unlike some of the following suggestions.

The method takes a value object, and two lamba expressions, one for converting the value object to the database, and one for converting the database value to the value object.
You then define the conversions yourself.

Below is an example. It is a value object for a name, based on a string.

```csharp
public class CourtName : ValueObject
{
    public string Get { get; }

    public CourtName(string name) => Get = name;
}

```

And the configuration, in the EntityTypeConfiguration class:

```csharp
entityBuilder.Property(court => court.Name)
                .HasConversion(
                    courtNameVo => courtNameVo.Get,
                    name => CourtName.Create(name)
                );
```

The first lambda is from Value Object to database value, i.e. just take the wrapped string.\
The second lambda is from database value to Value Object, i.e. create a new Value Object from the string.\
If you are using Result, you will need a method to extract the created object from the Result. 
You may consider it reasonable to not check whether the name is valid. This was probably checked before the data went into the database.

