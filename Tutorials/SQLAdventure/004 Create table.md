# Creating your first table

In the following, write and execute all SQL statements in the console, we have used so far.

Each statement can be highlighted, and executed in isolation.

### The case
The StarCompany wants to keep track of their employees and data related to them.\
Therefore, you are going to make a table in the database with this data represented.\
The table will include information about the employees' first name, last name, 
which department they are attached to, their salary (per month) and a unique identification number for each person.

### Creating the table

Execute the following SQL command to create a table for the data:

```sql
CREATE TABLE employee(
    firstname varchar(15), 
    lastname varchar(15), 
    department varchar(3), 
    salary bigint, 
    employid smallint
);
```

Above, we have created a table called "employee", with some information about each employee.
Similar to `int` in Java, `bigint` and `smallint` are data types, just with various number of digits. The type `varchar` is your basic `string`.

