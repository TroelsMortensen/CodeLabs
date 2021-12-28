# Insert employees

Next up, we will add a couple of employees to the database. Do this by executing SQL commands in the console, as before.

You must insert at least 6 different employees, and they should belong to at least 3 different departments, e.g. "A1", "A2", "A3".\
A department is just a 3-character code.\
Remember, the data type was `varchar(3)`, meaning a string of maximum 3 characters.

The following statement will insert a single employee:

```sql
INSERT INTO Employee (firstname, lastname, department, salary, employid) 
Values ('Anders', 'Hansen', 'A2', 18900, 24);
```

This is Anders Hansen, employed to department "A2", with a salary of 18900 kr/month. His employee ID is 24.

Insert another five employees.