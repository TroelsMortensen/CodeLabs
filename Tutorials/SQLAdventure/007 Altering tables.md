# Altering a table

Since you created the employee table, the StarCompany management has decided they also wants to register the date, where each employee was hired.\
For this, we need to make a modification to the employee table.

### Adding start date
Execute the following statement to add another column:

```sql
ALTER TABLE employee add startdate DATE;
```

We are saying we want to change the table called employee. We want to add a new column (attribute/field/property) called `startdate`, and the data type is `DATE`.

### Adding age
The company also want the age of the employee. Add another column of type `smallint` to hold an employee's age.

See the answer below

<details>
<summary>Hint</summary>

```sql
ALTER TABLE employee add age smallint;
```

</details>

### More employees
Add another 3 employees, you can reuse the previous `INSERT` statement, but need to modify it to match the updated table data, i.e. include age and startdate.\
A date is formatted like this `'yyyy-mm-dd'`, so e.g. `'2007-04-16'`.