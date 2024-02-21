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

.property("someValue")...
```

They do the same thing. If you can do lambda, that is compiler safe, i.e. if you rename the property/field, the compiler will remind you to update the configuration. Or if you do rename by refactor, the configuration is also updated.

If you do the string, you have to remember to update your configuration as well. Here it is nice to have unit tests covering such cases. 