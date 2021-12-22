# Updating existing values

Department "A1" is doing very well, and so they have all been granted a pay raise of 10%.

Use a `UPDATE` statement in increase the pay of all employees in department "A1" (or another one, with more than one employee) by 10%.

Give a thought yourself first, and see if you can figure it out. If you're stuck, theres a hint below:

<details>
<summary>hint</summary>

```sql
UPDATE employee
SET salary = salary * 1.1
WHERE department='A1';
```

</details>

