# Value Objects

There are different ways to configure value objects, based on various factors:
* Is the property nullable or not?
* Does the value object contain one or more values?
* Does the value object consist of other value objects, i.e. nested?
* Is it a list of value objects?

For a long time configuring value objects used a technique called Owned Entity Type. And we would generally do all above cases with this approach.
It is basically a hack, but was the only available option.

In EFC8, they introduced Complex Types, which is perfect for value objects. 
However, the feature is unfinished and limited. Hopefully EFC9 improves upon things, but that is currently no use to you, as EFC9 probably is released in the fall of 2024.\
I should remember to update this guide accordingly.

The next couple of slides cover the various cases of value objects. 