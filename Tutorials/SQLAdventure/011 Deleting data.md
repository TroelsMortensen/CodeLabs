# Delete data from table

### Delete some
The company has way too many expenses and wants to get rid of all employees with a salary bigger than 300.000 **per year**.

The template-command for deleting:

```sql
DELETE FROM ?
WHERE ? ;
```

See if you can delete the specific employees.

<details>
<summary>hint</summary>

```sql
DELETE FROM employee
WHERE salary * 12 > 300000;
```

</details>


### Delete more

In the meantime things are going much worse! All employees hired later than 2006 must be fired.

<details>
<summary>hint</summary>

```sql
DELETE FROM employee
WHERE EXTRACT(year FROM startdate) > 2006;
```

</details>

### And even more

Now things go completely wrong! The whole department A3 must be removed! \
Delete every employee in department A3 (or some other department).

<details>
<summary>hint</summary>

```sql
DELETE FROM employee
WHERE department = 'A3';
```

</details>