# Insert data from query result
Sometimes you wish to insert data based on a query result. Perhaps you are given a messy table with all data, and wish to split it out into separate tables.

Assume again the below Employee table, and another table, Names:

![EmployeeAndNames](EmployeeAndNames.png)

We now wish to take the `firstname` and `lastname` from all rows of the Employee table, and insert that data into the Names table.  
We can do this by first getting the required data with a query, and then using that as the data to insert:
```sql
INSERT INTO Names(f_name, l_name)
(SELECT firstname, lastname FROM Employee);
```
The second row is the query which returns the firstname and lastname of all rows in Employee. This result is then inserted into Names. Notice that the number of columns queried and inserted into, must be the same. The same is true for the data type of the columns, e.g. f_name and firstname must have the same type.

_**[Here](https://www.postgresql.org/docs/12/dml-insert.html)**_ you can find some introduction examples on the postgresql site. 

The postgresql documentation for `INSERT` is found _**[here](https://www.postgresql.org/docs/9.5/sql-insert.html)**_. About half way down you will find some examples.