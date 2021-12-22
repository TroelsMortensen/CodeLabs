# Query data

### Who works in department A3?
Write a statement to retrieve all employees, who work in department "A3" (or another, if you don't have A3).\
Use the `SELECT` statement

<details>
<summary>hint</summary>

```sql
SELECT *
FROM employee
WHERE department = 'A3';
```

</details>

### Who has a high salary?

Write a `SELECT` statement to retrieve all employees, who have a salary higher than 19000 (or some other number, which will return more employees).

<details>
<summary>hint</summary>

```sql
SELECT *
FROM employee
WHERE salary > 19000;
```

</details>

Now, the management is too lazy to count the number of rows themselves, so they just want to know how many employees have a pay highger than 19000.

We can do that with the following statement:

```sql
SELECT COUNT(*)
FROM Employee
WHERE salary > 19000;
```

The `COUNT(*)` will just count how many rows are returned.

See if you can figure out, how to write a statement, which will calculate the average pay of all employees.

<details>
<summary>hint</summary>

```sql
SELECT AVG(*)
FROM employee
```

</details>

### "Complex" `WHERE`

The company expanded april 1st, 1997, and wants to celebrate that. 
Make a list of employees that were hired before the expansion 
and get less than 21000 per month in salary (or some other number).

Use the `SELECT` statement. When you have more than 1 condition you separate them with an `AND`.

<details>
<summary>hint</summary>

```sql
SELECT *
FROM employee
WHERE startdate < '01-04-1997' 
  AND salary < 21000
```

</details>

