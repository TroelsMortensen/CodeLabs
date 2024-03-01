# DDD With EFC

This guide aims to explain how to configure Entity Framework core to work with your DDD inspired Domain Model.\
Such a domain model looks vastly different from what EFC conventionally uses, 
which are just simple classes (data containers), 
with public properties.

Your domain model uses value objects, entities, aggregates, strongly typed ids, and much more.\
The patterns used to construct this domain model differs from the conventional EFC approach,
and therefore we need to do a lot of manual configuration so that EFC can persist your data.

For the most cases, we can do this configuration without interfering with the DM.\
There will, however, be some cases, 
where you may have to rework your domain model a little bit. Mainly internal parts, so this should hopefully be acceptable.\
This is a trade-off we must accept, if we wish to use EFC. It does seem like they continuously improve support, though. Lucky us.

The next couple of slides does the initial setup in a step-by-step.\
And then follows slides, which deal with the various specific cases.

So, go through the first slides on setting up. Then find the slides relevant to your case.

Each specific configuration case is explained with an isolated, generic example, 
along with one or more unit tests proving correctness. Hopefully you can convert this to your own specific needs.

## Fluent API
EFC uses 3 kinds of configuration:
* By convention: Here you create "property bags", i.e. a class with public properties. You follow naming conventions. EFC will then discover most things.
* By annotations: Here you put attributes on your properties, e.g. [Key] and [Required].
* Here you write code in your DbContext class, to configure the entities.

We will go with the last option. Why? Our domain model class are _not_ property bags. They are carefully designed.\
Option two is "invasive", meaning we need to put all kinds of EFC specific attributes into the domain model. But remember,
we aim to keep technologies out of the domain.

The third approach means we can configure everything from the "outside". It is also the most powerful, and it takes precedence over the other two approaches.

It is called the "Fluent API", because you will often chain multiple methods together, using the Fluent technique.

For example:

```csharp
modelBuilder.Entity<Guest>().Property(guest => guest.Username).IsRequired();
```

The `modelBuilder` is the class used to get started with any configuration.\
We then want to configure something for the `Guest` entity.\
And we grab the property on the `Guest` called `Username`.\
Finally we say that this property must have a value, it is required.

So, this chain of method calls is "fluent". 

There are different "builders" in EFC, we will use different ones, for different purposes.\
If we wish to configure an entity, we use a ModelBuilder.\
If we wish to configure a property, we use a PropertyBuilder.\
And so on. This is somewhat "automatic". You will see.

Often configuration code (or generally fluent code, like LINQ) will be formatted so that each dot "." starts on a new line,
like this:

```csharp
modelBuilder.Entity<Guest>()
    .Property(guest => guest.Username)
    .IsRequired();
```

This seems to be a common approach, with the aim to increase readability.

I will also use this approach, mainly because it is easier to explain what each line does.
E.g. "line 2 accesses the Username property on Guest".

## How to read
A lot of the configurations are very similar, or we are doing part of the same thing over and over. 
This means, it is better described the first time, it is encountered.\
I realize this is unfortunate, if you use this guide as a reference, and you are looking for a specific case. Then that case may not actually contain all the details, you hoped for.\
Eventually I may rework this guide, so that each case is self-contained. But for now, I will just refer to the first time, and then be brief the next times.

## Table of content
Slides:
6. Constructor
7. Test helper methods
8. Guid primary key
9. Strongly typed primary key
10. Primitive field variables
10. Value object overview
11. Non-nullable single-valued value object
12. Nullable single-valued value object
13. Non-nullable multi-valued value object
14. Nullable multi-valued value object
15. Non-nullable nested value objects
16. Nullable nested value objects
17. List of Value objects
18. Single foreign key with Guid
19. Single foreign key with strongly typed id
20. List of Guid foreign keys (wrapper med conversion)
21. List of strongly typed ids (wrapper og conversion)
22. Enums
23. Enumeration classes
24. Single nested entity
24. List of entities.
