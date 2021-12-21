# Updating existing data

We often need to update existing data. This slide contains a couple of examples.

The basic syntax for updating is as follows:

```sql
UPDATE table_name
SET column_name_1 = data_value_1
    [,column_name_2 = data_value_2]
[WHERE condition]
```

The parts within [ ] are optional. E.g. you can update the values in one or multiple columns. 
You can also specify a `WHERE` condition, so only matching rows are updated. Otherwise, all rows are affected.

Below we update the employee with `employ_id = 24` so that their salary is 25000.

```sql
UPDATE employee
SET salary = 25000
WHERE employ_id=24;
```

You can expand on the `WHERE` condition with more constraints, as needed.

In the following we update three employees, specifically the ones with `employ_id` 17, 25, and 23. 
The idea is that these employees moved to another department, identified by 'A9'. The start date is also updated.

```sql
UPDATE employee
SET department = 'A9',
    startdate=CURRENT_DATE
WHERE employ_id IN (17, 25, 23);
```

What if we want to give every employee a 3% salary raise?

```sql
UPDATE employee
SET salary = salary*1.03;
```

But, maybe the managers deserve a bit more, say, another 5% raise:

```sql
UPDATE employee
SET salary = salary*1.05
WHERE position = ‘Manager’;
```

Let us promote one of our employees, 42, to a manager position, including a change to salary:

```sql
UPDATE employee
SET position = ‘Manager’, salary = 27000
WHERE employ_id = 42;
```

Again, you can find some extra examples _**[here](https://www.postgresql.org/docs/13/dml-update.html)**_.

And the full documentation _**[here](https://www.postgresql.org/docs/9.1/sql-update.html)**_.