# Updating existing data

The first 6 employees you inserted into the database does not have age or start date, because this information was added afterwards.

So, we have to update the first employees, to fill in this data.

You can update an employee with the following statement:

```sql
UPDATE employee
SET startdate = '2007-04-16'
WHERE employid = 24;
```

Here we are saying, we want to set the start date of the employee with id 24.

Now, update a couple of employees with various ages and start dates.

To verify, you can _query_ the database for data with

```sql
SELECT *
FROM employee;
```

