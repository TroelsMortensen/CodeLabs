# Inserting data
There are a few different ways of inserting data into the database. The first step here is to insert data by explicitly specifying the data.

Assume the following table, and columns

![Employee](Employee.png)

We wish to insert a new `Employee`, and can do so in different ways.

### Explicit, all columns

```sql
INSERT INTO Employee(firstname, lastname, department, salary, employ_id, startdate, age)
VALUES('Troels', 'Mortensen', 'SW', 5000, 42, '01-08-2016', 34);
```

In the first line we specify we wish to `INSERT` data into the `Employee` table. We also provide a list of the columns, we wish to insert data into.  
In the second line, we define what `VALUES` to insert.

### Implicit, all columns
The second approach can be used, if you just wish to insert data into all columns. The result will be the same as above.

```sql
INSERT INTO Employee
VALUES('Troels', 'Mortensen', 'SW', 5000, 42, '01-08-2016', 34);
```

Here we have left out the column names, and so it is implicit that all columns are chosen, and the order is the same is in the table definition.

### Explicit, some columns
You may wish to only insert data into some of the columns. Instead of using either of the above, and providing empty (e.g. `null`) values for some columns, you can instead specify which columns, you wish to insert data into. Like so:

```sql
INSERT INTO Employee(firstname, lastname, department, employ_id, startdate)
VALUES('Troels', 'Mortensen', 'SW', 42, '01-08-2016');
```
In the first line, we have specified the five columns for which we want to provide data. In the second row, the values are given. All columns not mentioned above will be set to `null`

### Multiple inserts
You can insert multiple rows (in this example Employees) into the table with one statement, if you comma-separate the provided values like so:

```sql
INSERT INTO employee(firstname, lastname, department, employ_id, startdate)
VALUES ('Troels', 'Mortensen', 'SW', 42, '01-08-2016'),
       ('Peter', 'Jensen', 'GBE', 57, '01-02-2018');
```
Here, two sets of data are provided, separated with a comma.