# Apply a Migration

The next step is to apply the migration to your database. Currently we have no database, so it will be created.

Again, in the terminal, and in the EfcData directory, we use the following command:

```
dotnet ef database update
```

![img_9.png](img_9.png)

In your EfcData project, you should now be able to see a new file, `Todo.db`, with a little database icon. If not, you may need to collapse and expand the EfcData project, i.e. click the little arrow next to the project. This will make it reload the content.

![img_10.png](img_10.png)

This `Todo.db` file is the Sqlite database. It's just a single file.

## Inspecting the Database

Rider has a built-in mini-version of DataGrip. If you double click the Todo.db file, you should see a wizard for adding a database source.

![img_11.png](img_11.png)

You can test the connection, to make sure the information is correct ((1)). If this is your first time, you may not have the Sqlite drivers installed, and you should instead see a link to do so.

When clicking <kbd>OK</kbd>, it should open the Database window in Rider. This can also be found on the right side menu bar, or here:

![img_12.png](img_12.png)

In the Database window, you get something similar to DataGrip, where you can inspect your database:

![img_13.png](img_13.png)

You can also double click on tables, to see their content, if you want to verify changes made to the data in the tables.