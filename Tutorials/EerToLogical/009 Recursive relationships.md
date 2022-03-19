# Recursive Relationships

The rules are similar to those the 1:1 binary relationships just described previously.

## 1..1 : 1..1

![](1to1rec-mandatory.svg)

Create a copy of PK in the relation. It is an FK pointing to a PK of another occurrence (instance) in the same relation.

Result:

![](1to1-man-rec.png)


#### Note
You will probably never implement this, as it gives a chicken-egg situation:
* You cannot insert a row, with an empty FK, as the FK _must_ point to something; it is mandatory
* The first row inserted cannot reference anything, as no other row exists

So, these are mainly theoretical.

## 0..1 : 1..1

An EntityA occurrence may or may not reference another occurrence of EntityA.

![](1to1-man-op-rec.svg)

The result is the same as above, we introduce a forign key to point to the primary key

![](1to1-man-rec.png)

#### Note
When dealing with 0..1 : 1..1 you may also use the solution for 0..1 : 0..1, if you prefer.

This depends on how many `null`-values you end up with, if you go for the above approach. Too many `null`s, and you should consider using the below described pairing-relation.

## 0..1 : 0..1

Optional on both sides:

![](1to1-op-op-rec.svg)

**Example**: A student _may_ tutor another student, maximum one tutee. 
A student _may_ be tutored by another student, maximum tutor.

**Solution**: Create a new relation to track pairs.

![](1to1-op-op-relation.png)

## 1:*

You may choose either of the previous two approaches:

1) Create new foreign key attribute in the Entity
2) Create new paring table

Again, consider the number of `null`-cells.

## \*:*

This must be mapped with a new relation to track pairs. I.e. same approach as 0..1 : 0..1.
