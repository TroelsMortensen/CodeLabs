# Define Primary Keys and Constructors

As mentioned on the previous slide, you hopefully get an error about an entity missing a primary key.\
All entities must have a primary key defined. And all entities must have a constructor, which EFC can use.\
Initially you will just set this up on your aggregates, until EFC does not complain.

So, this is the next step.

You will have to work your way step by step through the entities, until you get a script printed in the console.\
Then as you move through configurations, you may get back to this error over and over, for each entity.\
Generally, I think EFC is decently good at outputting informative error messages. Get used to reading these.

First, a bit of structure.

## Preparing for configuration

We start with the first entity it is complaining about, in my case the `VeaEvent`, probably because it's the top DbSet defined.

We configure everything from inside the `OnModelCreating()` method. 
We are going to have _a lot_ of configuration, so we start out with an initial structure\
You have (at least) two approaches:

* Configuration method per entity, it's simpler, we go with this.
* Auto-discoverable configuration class per entity. A bit more complex, generally better.
You must create an EntityConfiguration class, [see here](https://www.entityframeworktutorial.net/code-first/move-configurations-to-seperate-class-in-code-first.aspx).

You can pick either approach. I will go with the first.\
Expand your code in the DbContext with a new method for configuring this first entity:

```chsparp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    ConfigureVeaEvent(modelBuilder.Entity<VeaEvent>());
}

private static void ConfigureVeaEvent(EntityTypeBuilder<VeaEvent> entityBuilder)
{
    throw new NotImplementedException();
}
```

You will create more helper configuration methods, all called from the `OnModelCreating()`.