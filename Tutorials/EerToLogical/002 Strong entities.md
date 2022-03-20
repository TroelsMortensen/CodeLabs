# Strong Entities

An entity is considered strong if it can exist without depending on other entities.

The following is an arbitrary entity:

![Entity](ArbitraryEntity.svg)

`attr_a_1` is primary key.\
`attr_a_2` is a simple attribute.\
`attr_a_3` is marked as alternate key (or candidate key), to indicate it is unique. It could be an email or phone number.\
`attr_a_4` is multivalued.\
`attr_a_5` is a composite attribute.\
`attr_a_6` is a derived attribute.

When mapping to a relation, keys, and simple attributes are included. 
Composite attributes are "flattened", i.e. we get a simple attribute per attribute in the composite.

Multivalued attributes are handled in a later step, i.e. left out for now.
This is because the relational database handles multivalued attributes in a special way. Remember that each cell in a table should contain a single value.

Derived attributes are also included, and marked as derived.

The resulting relation looks like this:

![img.png](img.png)

If no Primary Key was marked on the entity, you may introduce a new _surrogate key_. This is just a new attribute, you create. 
These are often just: movie_id, person_id, department_id, etc.

For the derived attribute, you might explain the calculation in the parenthesis, e.g. 
* **Derived**: age ( currentDate() - date_of_birth )
* **Derived**: movie_rating ( average of all ratings)