# Value Objects

There are different ways to configure value objects, based on various factors:
* Is the property nullable or not?
* Does the value object contain one or more values?
* Does the value object consist of other value objects, i.e. nested?
* Is it a list of value objects?

For a long time configuring value objects used a technique called Owned Entity Type. And we would generally do all above cases with this approach.
It is basically a hack, but was the only available option.

In EFC8, they introduced Complex Types, which is perfect for value objects. 
However, the feature is sort of unfinished and limited. 
Hopefully, some future update will improve upon things. If that happens, I will update the guide.\

The next couple of slides cover the various cases of value objects. 

**In general, if you _don't_ require nullability, then go with the complex type approach!**

### How to read
The following slides will show different ways to configure value objects. 
The first slides are more elaborate in detail regarding what the code does.
Then, because the configuration is pretty similar, the later slides will be more brief.

### Has Conversion
The most versatile and simplest approach to mapping Value Objects is to use the HasConversion method.\
You already saw this in the strongly typed ID mapping. It can work for any single-valued value object.\
If you are creative, you may also be able to use it for multi-valued value objects, but I would not recommend it. Querying becomes difficult.

You can see an example of single-valued value object again on the next slide. 

### Note
For each field variable or property on an entity, we have to add specific configuration.\
You can access this field or property in two ways: 
* with a lambda expression pointing to the property, like we did for the IDs
* with a string, containing the name of the field variable.

I will generally use the second approach, as your fields are _supposed_ to be private.\
However, if you make them internal, and let the Data Access project get access, you can still use lambdas.

It looks like this:

```csharp
.property(entity => entity.someValue)...

or:    

.property("someValue")...
```

They do the same thing. If you can do lambda, that is compiler safe, i.e. if you rename the property/field, the compiler will remind you to update the configuration. Or if you do rename by refactor, the configuration is also updated.

If you do the string, you have to remember to update your configuration as well. Here it is nice to have unit tests covering such cases. 