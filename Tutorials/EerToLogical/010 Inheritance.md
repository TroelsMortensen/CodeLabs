# Inheritance

There are 4 versions when dealing with inheritance. 

Which to pick is determined by the inheritance-constraints (participation and disjoint), as there are four combinations:

* {mandatory, and}
* {mandatory, or}
* {optional, and}
* {optional, or}

Each combination above has a recommended approach to mapping.\
However, sometimes you may have good reason to use an approach other than the recommended.

## {mandatory, and}

It could look like this.

![](MandatoryAnd-EER.svg)

You create a single relation to cover the super- and sub-entities.
You add attributes to indicate whether a row is **SubA** or **SubB** or **SubAB**, this attribute is called a discriminator.\
Usually the sub-types do not define primary keys, and so the primary key is commonly just the BaseEntity's primary key.\
Other Primary Keys are marked as Alternate Keys {AK}.

You can either add a boolean attribute for each sub-entity, to say whether the row is A, B, AB, or something else.

Or you can just add a single attribute, which can indicate which combination of sub-entities are used.

The result is either of the below:

![](ManOr-Relation.png)

If there are many sub-entity-types, a single attribute discriminator may be easier to deal with.

## {mandatory, or}

![](ManOr-ER.svg)

In this case, it doesn't make sense to combine the Sub-types, because the disjoint constraint is "or". It would lead to many null values.

The solution is to create many relations: one per combination of Base-SubType. I.e. you will get a number of relations equal to the number of sub-types.

In the above, we would get two relations: Base-SubA, and Base-SubB.

Result:

![](ManOrRelation.png)

## {optional, and}

![](OpAnd-ER.svg)

Here, we make two relations: 
* one for the super-entity
* one for all sub-entities, with discriminator attribute(s) to distinguish the rows.

The primary key of the relation for the sub-entities will reference the primary key of the base-relation.

The result:

![](OpAnd-relation.png)

Here the `subType` attribute tells which type of combination of A, B, or AB it is. Similar to the case for **{mandatory, and}**.\
Alternatively a number of boolean attributes could be used.

## {optional, or}

![](OpOr-ER.svg)

This is handled with a relation per entity: Base, SubA, and SubB.

The primary keys (if non are present) will be a copy of the primary key attribute of the super-entity.\
The sub-relation will then reference the super-relation.

Result:

![](OpOr-relation.png)

