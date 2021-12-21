# Deleting data
We can remove all kinds of data:

* rows
* tables
* schemas
* domains
* constraints

The syntax is somewhat similar, except for rows, we will do those on the next slide.

If you wish to delete any of the other types of data, we use the `DROP` keyword:

```sql
DROP TABLE [IF EXISTS] employee [CASCADE];
```

This will delete the table `employee`. If the table contains any rows, you will get an error. 
You can then append `CASCADE`, meaning all rows will be deleted as well. Be careful with this.

If the table you are trying to drop does not exist, you will get an error. You can include `IF EXISTS` to only drop the table, if it actually exists.

More information about dropping tables _**[here](https://www.postgresql.org/docs/9.1/sql-droptable.html)**_.

In a similar way you can drop other things. A full list can be found _**[here](https://www.postgresql.org/docs/9.1/sql-commands.html)**_. It is an overview of all commands, just scroll down to the `DROP [SOMETHING]`.

