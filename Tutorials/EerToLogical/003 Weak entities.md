# Weak Entities

Weak entities are the kind which cannot have an instance without referencing another entity instance. Examples:
* A Profile cannot exist without a User
* A Ticket cannot exist without Customer and Concert
* A Room cannot exist without a Building
* A Playlist cannot exist without a User

Weak entities are mapped similar to strong entities. 
They will at a later point include one or more foreign keys. 
This is done when mapping relationships.

A weak entity results in a relation.

* Include all simple attributes
* Composite attributes are broken into simple attributes
* The primary key is partially or fully derived from the owner entity. This means we cannot define the primary key until after all relationships are mapped.

### Example

![WE](WeakEntity.svg)

In the above entity, `attr1` is marked as {PPK}, to indicate this attribute is part of the Primary Key (partial primary key), but is not enough in itself. As it is a weak entity, we need to include one or more foreign keys in a composite primary key. This is done in a later step.

The resulting (currently unfinished) relation:

![](WeakRelation.png)

The primary key is unfinished, so currently we have included a temporary placeholder sa `?`.
