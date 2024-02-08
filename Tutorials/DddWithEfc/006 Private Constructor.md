# Private Constructor

Though, initially EFC will complain about a missing primary key, it will then complain about a missing constructor.\

But the latter is easier to fix, so we start there.

Every entity, which EFC must manage, must have a constructor it can use, when materializing entities from the database back to C#.

Such a constructor must either match all relevant properties, which won't do for us, because DDD DM, **or**, 
it must be a no-argument constructor:

```csharp
private MyEntity(){}
```

Like this. EFC uses a lot of reflection, so it is not a problem, that it is private. Now the object can be instantiated, and relevant properties are set through reflection.\
We must add a private constructor to every EFC-entity, i.e. every kind of class EFC will manage, including value objects.\
EFC deals only with "entity", everything is an entity. DDD is more granular, with aggregate, entity, value object.\
I hope I can keep this straight, and not confusing.

The point is, when EFC complains about a missing constructor, you will add a boring, private, no-args constructor.

We find this case of EFC seeping into our sacred DM acceptable, as we do not change the public API of the entity. 
We are not using anything EFC specific, like attributes [Required]. We are not changing behaviour, or anything of relevance.\
It's just a tiny detail, we must remember, or be reminded by EFC.

So, go ahead and add a private constructor to the first entity.

