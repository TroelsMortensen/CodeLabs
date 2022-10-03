# \*:* Relationships

![](ManyToMany-ER.svg)

You should already have relations for `EntityA` and `EntityB`, mapped on slide 2 or 3.

You need a new relation to track each pair of EntityA-EntityB. This relation will also contain any relationship attributes.\
We call this a "join-relation", or "join-table". Or sometimes "relationship-relation", but that sounds silly.

The primary key of each relation will be posted into the join-relation, and be foreign keys, each pointing back to a relation: EntityA or EntityB.

The join-relation's primary key will be the combination of the two foreign keys (usually). There may be special cases where the primary key of the join-relation will contain some of the relationship attributes too.

Result:

![](manytomany-relation.png)

