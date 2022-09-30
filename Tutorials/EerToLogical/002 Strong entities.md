# Strong Entities

An entity is considered strong if it can exist without depending on other entities.

The following is an arbitrary entity:

![Entity](ArbitraryEntity.svg)

`attr1` is primary key.\
`attr2` is a simple attribute.\
`attr3` is marked as alternate key (or candidate key), to indicate it is unique. It could be an email or phone number.\
`attr4` is multivalued.\
`attr5` is a composite attribute.\
`attr6` is a derived attribute.

When mapping to a relation, we include keys and simple attributes. 
Composite attributes are "flattened", i.e. we get a simple attribute per attribute in the composite.

Multivalued attributes are handled in a later step, i.e. left out for now. See slide 14.
This is because the relational database handles multivalued attributes in a special way. Remember that each cell in a table should contain a single value.

Derived attributes are also included, and marked as derived.

The resulting relation looks like this:

![img.png](img.png)

If no Primary Key was marked on the entity, you may introduce a new _surrogate key_. 
This is just a new attribute, you create. 
Often just called `id`.

For the derived attribute, you might explain the calculation in the parenthesis, if it is not too complex, e.g. 
* **Derived**: age ( currentDate() - date_of_birth )
* **Derived**: movie_rating ( average of all ratings)