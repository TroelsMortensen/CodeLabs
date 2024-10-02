# 1:1 relationships
In this section, we will cover the one-to-one relationships.\

From the ER diagram, we can see that the `Book` and `PriceOffer` entities have a one-to-one relationship.

The Book has a price, but sometimes it's on sale, in which case there will be a PriceOffer instance, with a NewPrice and a PromotionalText.\
Whenever the sale ends, the PriceOffer is removed, and the Book's price is the regular price. Yes, this is a simple example, but it's enough to show the concept.

Here's the part of the diagram, we will focus on:

![img_1.png](img_1.png)

Now, let's implement this in code. First, we do the Book, without the relationship. Then the PriceOffer without relationship, then we connect them