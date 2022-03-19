# \*:* Relationships

![](ManyToMany-ER.svg)

You should alread have relations for `EntityA` and `EntityB`.

You need a new relation to track each pair of EntityA-EntityB. This relation will also contain any relationship attributes.\
We call this a "join-relation", or "join-table". Or sometimes "relationship-relation", but that sounds silly.

The join-relation will have two foreign keys, each pointing to a relation.

The join-relation's primary key will be the combination of the two foreign keys.

Result:

![](manytomany-relation.png)

