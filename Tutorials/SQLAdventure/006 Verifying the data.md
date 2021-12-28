# Verifying the data

Now that we have inserted employees into the table, we just want to verify the data is actually present.

To retrieve data from tables, we use a `SELECT` statement. These can be somewhat advanced, depending on what kind of data you want to retrive.\

We will start simple.

Execute the following select-statement

```sql
SELECT *
FROM employee;
```

This will _query_ the employee table, and retrieve everything, because of the *.

You should at the bottom in DataGrip see the result as a table with the employees.

Alternatively, you can also double click on the table in the browser view to the left, nested under the starcompany schema.
