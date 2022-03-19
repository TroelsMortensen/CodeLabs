# Complex Relationships

These are mapped similar to the \*:* relationship: We use a join-relation.

Consider the following EER:

![](ComplexRelationshipER.svg)

Here we have three entities involved in a relationship, and also with relationship attributes.

The multiplicity is irrelevant when dealing with complex relationships, the result is the same.

Again, you should already have relations for the three: EntityA, EntityB, EntityC.

We create a join-relation for the relationship. It includes the three primary keys of A, B, and C.\
The primary key of the join-relation is the composite key made up of the three.\
Relationship attributes are included in the join-relation.

Result:

![](ComplexRelations.png)