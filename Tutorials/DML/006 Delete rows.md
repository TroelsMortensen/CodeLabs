# Deleting rows
You can delete all rows from a table with the below command. This will not `DROP` the table itself, just remove all data in it.

```sql
DELETE FROM <table name>
[WHERE condition]
```

You must specify the table name. If no `WHERE` clause is included, all rows are removed.

If you include a `WHERE` condition, only matching rows are removed.

Say the employee with id 24 resigns their job. We will then remove them from the database:

```sql
DELETE FROM employee
WHERE employ_id = 24;
```

More information can be found _**[here](https://www.postgresql.org/docs/9.1/sql-delete.html)**_.

### ON DELETE constraints
When deleting something, you may get constraint errors, if you have not defined `ON DELETE` behaviours of foreign keys point to the table, you are deleting from.

For example, assume you have two tables, `albums` and `images`:

![img.png](img.png)

It is a 1:* relation, notice image has a foreign key reference to `album(album_id)`.

We could have the following data in `albums`:

|     id    |     title            |     description                                              |     date_created    |
|-----------------|----------------------|--------------------------------------------------------------|---------------------|
|     1           |     quidem           |     quam nostrum impedit mollitia   quod et dolor            |     20-03-2019      |
|     2           |     sunt qui         |     ut pariatur rerum ipsum natus repellendus praesentium    |     12-03-2017      |
|     3           |     omnis laborum    |     et rem non provident vel ut                              |     25-11-2018      |
|     4           |     non esse         |     id non nostrum expedita                                  |     11-01-2013      |

and in `images` we could have:

|     id    |     title          |     description                 |     date_created    |     url                                       |     album_id    |
|-----------|--------------------|---------------------------------|---------------------|-----------------------------------------------|-----------------|
|     1     |     odio           |     aut ipsam quos              |     24-09-2011      |     https://via.placeholder.com/600/323599    |     1           |
|     2     |     voluptate      |     ut esse id                  |     19-05-2012      |     https://via.placeholder.com/600/1224bd    |     2           |
|     3     |     tenetur        |     et soluta est               |     19-03-2016      |     https://via.placeholder.com/600/a19891    |     3           |
|     4     |     expedita       |     quam quos dolor eum         |     26-06-2017      |     https://via.placeholder.com/600/224566    |     2           |
|     5     |     neque          |     magni nulla et   dolores    |     08-07-2017      |     https://via.placeholder.com/600/40591     |     3           |
|     6     |     praesentium    |     et corrupti nihil cumque    |     03-01-2018      |     https://via.placeholder.com/600/1fb08b    |     4           |
|     7     |     quidem         |     quod non quae               |     16-10-2019      |     https://via.placeholder.com/600/14ba42    |     1           |


What would happen, if we tried executing the following command?

```sql
DELETE FROM albums WHERE id = 1;
```

It would result in an error message like the following:

> [2021-04-05 15:02:25] [23503] ERROR: update or delete on table "album" violates foreign key constraint "photo_album_id_fkey" on table "photo"
> 
> [2021-04-05 15:02:25] Detail: Key (id)=(1) is still referenced from table "photo".

It tells us we are violating a foreign key constraint, because rows in `images`, the images with ids: 1 and 7,  are referencing the album row we are trying to delete.

Either, we will have to "clean up" first, meaning we manually delete all images referencing the album, we want to delete.  
Or we should have added an `ON DELETE` behaviour when declaring the `images` table:

```sql
CREATE TABLE images (
    id SERIAL PRIMARY KEY,
    title VARCHAR(50),
    description VARCHAR(250),
    date_created DATE,
    url VARCHAR(250) UNIQUE,
    album_id INTEGER REFERENCES album(id) ON DELETE [CASCADE / SET NULL]
)
```

Here, if you pick:

* CASCADE: all rows in `images` referencing the deleted album, will also get deleted
* SET NULL: all rows in `images` referencing the deleted album, will get their `album_id` attribute set to `NULL`, i.e. they are no longer part of any albums.

This was discussed in the session about Data Definition Language.