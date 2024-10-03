# Introduction
This guide aims to explain:
* How to add EFC to your project
* How to configure domain entities by EFC convention
* How to interact with the DbSets of EFC

## The Example
I have <s>stolen</s> politely borrowed the example from the book "Entity Framework Core in Action" by Jon P Smith.

It resembles a book store, with books, authors, book categories, and reviews. The domain model is simple, but it's enough to show the basics of EFC.\
It also covers each of the relevant relationships:
* One-to-one
* One-to-many
* Many-to-many
* Many-to-many with relationships attributes

Here is the ER diagram:

![img.png](img.png)

The Book is the center. It belongs is described by multiple Categories (e.g. Sci-Fi, Romance, Autobiography, etc), has one or more Author(s), and can have multiple Reviews.\
Sometimes the Book is on sale, in which case there is a PriceOffer.

A Book can be written by multiple Authors, in which case we might want to keep track of the order of the authors for this specific Book. That's the purpose of the Writes::Order attribute.

For example, the book "BDD in Action" is written by two authors, and the order is important, because the first author is the main author.
If we also include the Foreword author, we might want to list him last.

## By Convention?
What does this mean?

EFC has 3 ways to configure the domain model:
1) By convention
2) By attributes
3) By fluent API

We will focus on the first method, it is the simplest.

It means, that if we follow a set of rules when naming our classes and properties, EFC will figure out the relationships between the entities, and primary, and foreign keys.\
This convention makes it easy to get started, because EFC will do most of the work for us.

Sometimes, though, we need to override the convention, or be explicit, 
because EFC cannot figure out the relationship or property by itself. In these cases, we can use the other two methods. 
I will not cover these in this guide, other than how to define primary keys.\
You can do a lot of configuration with the fluent API, which will be introduced in my elective course: Domain Centric Architecture (DCA).

## Source code
The code for this guide can be found in my GitHub repository here.... // TODO: Add link