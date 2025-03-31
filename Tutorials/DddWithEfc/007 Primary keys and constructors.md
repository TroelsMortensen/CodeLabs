# Define Primary Keys and Constructors

As mentioned on a previous slide, you hopefully get an error about an entity missing a primary key, or a suitable constructor.\
All entities must have a primary key defined. And all entities must have a constructor, which EFC can use.\
Initially you will just set this up on your aggregates, until EFC does not complain.

Then you can start configuration, and then as you tell EFC about your other entities, it will again complain about primary keys and constructors.

So, this is the next step.

You will first have to work your way one by one through the aggregate roots, until you get a script printed in the console.\
Then as you move through your configurations to include owned entities, you may get back to this error over and over, for each entity.\
Generally, I think EFC is decently good at outputting informative error messages. Get used to reading these.

First, a bit of structure.

## Preparing for configuration

We start with the first entity it is complaining about, in my case the `VeaEvent`, probably because it's the top DbSet defined.

We configure everything from inside the `OnModelCreating()` method. 
We are going to have _a lot_ of configuration, so we start out with an initial structure.\
You have (at least) two approaches:

* Configuration method per entity, it's simpler, we go with this, it's simpler for the guide, but the below approach is better. You should do that.
* Auto-discoverable configuration class per entity. A bit more complex, generally better.
You must create an EntityConfiguration class, [see here](https://www.entityframeworktutorial.net/code-first/move-configurations-to-seperate-class-in-code-first.aspx). You should go with this. The example uses EntityTypeConfiguration, but I had to use the interface: IEntityTypeConfiguration.

Create an EntityConfiguration class for your first entity.

I will simplify and use the other approach, and expand my code in the DbContext with a new method for configuring this first entity:

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