# Removing an entire table

First, delete any leftover employees:

```sql
DELETE FROM employee;
```

Once the table is empty, you can remove the table itself:

```sql
DROP TABLE employee;
```

The `DROP` keyword is generally used to remove anything that isn't a row in a table.

### Removing the schema

Now that the company is shut down, and all employees are fired, we might as well remove the starcompany schema too:

```sql
DROP SCHEMA starcompany;
```


# Thanks for joining in on the SQL adventure!