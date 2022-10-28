# Configuring Relationships

An object oriented model have relationships between model classes, usually in the form of associations. Our domain classes are like this:

![img.png](Resources/DomainClasses.png)

This means, the Todo has a reference to its owner, a User. The Todo knows about the User, but the User does not know about the Todo.

We could have flipped the association, and said the User includes a list of all their Todos.

The intention is that **"a user can be assigned to many todos"**.

Usually in the object oriented domain, the relationships between classes are one-way, as above.

## Generating Tables
If we generate the database now, we will get a table per domain class, i.e. Todo and User.
We will get a relationship between them as shown in this EER-diagram:

![img.png](Resources/EER.png)

Notice there is no "Owner" on Todo, because that is implied by the relationship.

And if you know your databases, you know this is implemented in the database as these tables:

---
### Relational schema

**Todo**(Id, Title, IsCompleted, Owner)\
**Primary key**: Id
**Foreign key**: Owner **references** User(Id)

**User**(Id, UserName)\
**Primary key**: Id

---

So, the Todo gets a foreign key to the User.

This is what EFC will generate for us, whether we use the object oriented association of "Todo -> User" or "Todo <- User". Either way, it is a 1:* relationship, which results in the above relational schema.

## What is a Navigation Property?

The relationship is implemented in the database as a foreign key, and in the domain classes, we use an association, i.e.:
* Todo has an association (Owner) to User, or
* User could have a list of Todos

This association is called a "Navigation Property", it is used to navigate around between objects. 
Essentially it is an object oriented implementation of a foreign key. 

## Two-Way Navigation Properties

Currently, our domain classes have a one-way navigation property, i.e. Todo -> User.

Sometimes, you may want to make a two-way relationship, i.e. add Navigation Properties on both sides. This can vastly simplify some queries against the database, depending on what side of the data you start from. Example:

* If you want to load a Todo with the User, it is easy because the Todo includes the User.
* If you want to load a User with all its Todos, it is much more complicated, because Users do not know about their Todos.

In the latter example, you would have to go through all Todos, and check if their User is the one, you wish to load.

If we add a Navigation Property, User -> Todo (i.e. the list in User), it is easily loaded: Find a User, also load the associated Todos.

So, often it is a benefit to have two-way navigation properties, and this is commonly seen.

The classes would then look like this:

![img.png](Resources/TwoWayNavProp.png)

Notice the User has a collection of all Todos assigned to it.

However:

## Clean-like approach

Remember the discussion on the previous slide. We are again about the modify domain classes, because of an outer ring. 

### Consequences
This circular dependency can have unforeseen consequences because object oriented classes don't always do too well with these. If we keep some data in memory and wish to update the assignee of a Todo, we would have to modify both the Todo::Owner and the User::Todos, i.e. double work, with the potential of forgetting some updates.\ 
We might need this double work for both updates, deletes, creates. And with a larger domain, this can escalate.

JSON cannot be serialized with circular dependencies, so if we did the two-way, we could no longer use our implementation of the File storage. Now, maybe that's not a big concern, because the File storage functionality is in an outer ring, and shouldn't affect the domain. But still.

### The Fix?
If we really want to keep things separated, stick to the clean approach, we must not touch our domain classes.\
An alternative would then be do define a set of classes for the EFC layer only. The DAO interfaces would still work with Todo and User, but the EFC layer would convert these to TodoEFC and UserEFC as needed.\
These new classes would be sub-classes of the domain classes. And in these we specify the EFC-related stuff, e.g. attributes and relationships.

It could look like this:

![img.png](Resources/EfcDomainClasses.png)

The TodoContext would then contain DbSets of the Efc classes. 
We don't need a sub-class of Todo for now, but it might still be a good idea to creat one, 
in case of future changes to the system, which shouldn't affect Todo.

The TodoContext would then look like this:

```csharp
public class TodoContext : DbContext
{
    public DbSet<UserEfc> Users { get; set; }
    public DbSet<TodoEfc> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Todo.db");
    }
...
```

This would potentially create some translation work between logic and dao layer. E.g. you get a UserEfc from the database, but should return a User. On the other hand, if UserEfc extends User, that might not be a problem. The logic layer gets a User, and doesn't know it is actually a UserEfc.

I have seen a few semester projects use this approach with some success. However, I don't think this is a common approach, but instead people resort to modifying the domain classes.

We will just update the domain classes. 

[I have asked this question on reddit](https://www.reddit.com/r/dotnet/comments/yd1h0f/keeping_efc_navigation_properties_out_of_the/), maybe someone has given input, when you read this. Eventually this paragraph may get updated.\
See last section on this slide for more information.

## Update to Domain Classes
This means an update to the User class, it looks like this:

```csharp
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }

    public ICollection<Todo> Todos { get; set; }
}
```


## Shadow Properties and Constructors
There is another way, where we can still use the `User::Todos` of ICollection. Without actually having this in the User class.

It is a sub-set of what is called a Shadow Property, i.e. a property that does not really exist, but EFC sneakily adds it.

Basically:
* In the DbContext subclass, OnModelCreating method, we can add a property to a Domain class, e.g. User.
* This property does not exist outside of EFC territory.
* This property can still be used in LINQ expressions

So, we get what we want, without touching the Domain classes. That's neat. It's just a little extra work.

[You can read about it here](https://learn.microsoft.com/en-us/ef/core/modeling/shadow-properties#configuring-shadow-properties)

[This link says something about how we can use constructors, it might also be relevant](https://learn.microsoft.com/en-us/ef/core/modeling/constructors) 