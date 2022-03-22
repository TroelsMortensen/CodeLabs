# Weak Entities

Weak entities are the kind, which cannot have an instance without referencing another entity instance. Examples:
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
* Multivalues are left out for now
* Derived attributes are included and explained, as in the previous step

### Example

![WE](WeakEntity.svg)

In the above entity, `attr1` is marked as PPK, to indicate this attribute is part of the Primary Key (partial primary key), but is not enough in itself.\
As it is a weak entity, we (usually) need to include one or more foreign keys in a composite primary key.\
This is done in a later step, when the relationship is mapped.

The resulting (currently unfinished) relation:

![](WeakRelation.png)

The primary key is unfinished, so currently we have included a temporary placeholder: `?`.

Often, weak entities are implied by them having a relationship to the "owner entity" with 1..1 on the owner side. This implies the weak entity _must_ reference the strong entity.

#### Note
In theory the primary key of a weak entity will be a composite key, which will include the FK(s) that points to the owner(s)'s PK.

You may have a project with a name, run by a department, which has PK: dep_id.\
The project name itself may not be unique, but combined with the dep_id it can be a key, as a department will not create to projects with the same name.

Sometimes this results in large composite keys (i.e. containing many attributes), and it may be better to introduce a surrogate key.