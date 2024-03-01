# Private Constructor

Initially EFC will complain about a missing primary key, and it will then complain about a missing constructor.

But the latter is easier to fix, so we start there.

Every entity, which EFC must manage, must have a constructor it can use, when materializing entities from the database back to C#.

Such a constructor must either match all relevant properties, which generally won't do for us, because DDD DM, **or**, 
it must be a no-argument constructor:

```csharp
private MyEntity(){}
```

Like this. This is what we will do, whenever EFC complains.\
EFC uses a lot of reflection, so it is not a problem, that it is private. Now the object can be instantiated, and relevant properties are set through reflection.

We must add a private constructor to almost every EFC-entity, i.e. every kind of class EFC will manage, often including value objects. Though, you can hold off until EFC actually complains.\
EFC deals only with "entity", everything is an entity. DDD is more granular, with aggregate, entity, value object.\
I hope I can keep this straight, and not confusing.

The point is, when EFC complains about a missing constructor, you will add a boring, private, no-args constructor.

We find this case of EFC-needs seeping into our sacred DM acceptable, as we do not change the public API of the entity. 
We are not using anything EFC specific, like attributes [Required]. We are not changing behaviour, or anything of relevance.\
It's just a tiny detail, we must remember, or be reminded by EFC.

If we go away from EFC, we don't actually have to rework the DM again.

So, go ahead and add a private constructor to the first entity.

