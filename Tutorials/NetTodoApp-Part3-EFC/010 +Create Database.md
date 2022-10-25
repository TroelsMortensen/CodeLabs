# Create the Database

We have done the configuring and we are ready to let EFC generate the database for us.

First, we must generate a Migration. This is a file containing information about how to create/update the database.

Every time the domain classes change or a new class is added, we need to create a new migration, and apply it to the database.

In this way the database can be regularly updated.

## Modify Todo
First, we need to modify the Todo class. It currently looks like this:

```csharp
public class Todo
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }

    public bool IsCompleted { get; set; }

    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}
```

We have set properties for Id and IsCompleted, but not the other two. The are read-only. They are instead set through the constructor.\

The current setup with a constructor and some read-only properties is because of various code in the logic layer.\
EFC needs public set-properties. And apparently EFC cannot set navigation properties through the constructor, it must be through a public set-property.


So, this is a bit annoying, but I have not found a good way around it.\
We need to make changes to domain and logic.

The Todo class should be changed to:

```csharp
public class Todo
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; set; }

    public bool IsCompleted { get; set; }
}
```

This results in compile errors in `TodoLogic`, because we removed the constructor. Let's fix those.

First, line 27 in method `CreateAsync()`, where a new Todo is created. Change the instantiation to:

```csharp
Todo todo = new Todo()
{
    Owner = user,
    Title = dto.Title
};
```

Second, what is now line 72, in method `UpdateAsync()`, where a Todo is created. Change this instantiation to:

```csharp
Todo updated = new()
{
    Id = existing.Id,
    Owner = userToUse,
    Title = titleToUse,
    IsCompleted = completedToUse
};
```

##### Comment

The tutorial was written on the fly, and I did not originally know about this problem with EFC. Otherwise, we would have made the model classes originally without constructors. It is unfortunate.

## Generate a Migration

A migration is created through the terminal (or command line interface).

Open the terminal.

Navigate to the EfcData project. Most likely when you open the terminal, it is located in the solution directory. You want to enter the EfcDataAccess directory:
```
cd EfcDataAccessc
```

![](Resources/NavigateToEfc.gif)

Once there, type in the following:

```
dotnet ef migrations add InitialCreate
```

The last part, `InitialCreate`, is the name for the migration we are about to create. 
You should generally call it something, which indicates what this migration does, 
e.g. "UserEntityAdded", "TodoEntityUpdated", "EmailAddedToUser", or something similar. These names can be compared to Git commit message.\
Migrations are sort of a form of version control, similar to how you use Git.

Execute the above command. You should get:

```
PS C:\TRMO\RiderProjects\TodoAppWasm\EfcDataAccess> dotnet ef migrations add InitialCreate
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
PS C:\TRMO\RiderProjects\TodoAppWasm\EfcDataAccess>
```

Now, look in the EfcDataAccess component, you should see a new folder, Migrations. 

![img.png](Resources/MigrationsFolder.png)

Here is the "version control" of your database. These files keep track of the modifications to your code, which should eventually be applied to the database. And the files keep track of which of the migrations are actually applied. Maybe you have a few migrations, which have not yet been applied to the database.\
When you then update the database, EFC will figure out the difference, and apply the necessary migration(s).

This folder should probably also be under version control for your projects, so that when one group changes the database, the others can get the update.

#### Deleting the Migrations
Sometimes, you may want a "hard reset", if you somehow mess up. You can delete the Migrations folder, along with the database file generated on the next slide, and start over.

This can best be done with SQLite. When using other databases, go google how to revert a migration.

## Apply a Migration
The next step is to apply the migration to your database. Currently we have no database, so it will be created the first we apply an update.

Again, in the terminal, and in the EfcDataAccess directory, we use the following command:

```
dotnet ef database update
```

Like so:

```
PS C:\TRMO\RiderProjects\TodoAppWasm\EfcDataAccess> dotnet ef database update
Build started...
Build succeeded.
Applying migration '20221025111855_InitialCreate'.
Done.
PS C:\TRMO\RiderProjects\TodoAppWasm\EfcDataAccess>
```

Whenever a new migration is created (or multiple), you can do the above to apply them.

Again, in the EfcDataAccess component, you should now see your database file:

![img.png](Resources/DatabaseFileAdded.png)

## Update path

Finally, in the TodoContext, we have the piece of code, which points to the db file:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite("Data Source = Todo.db");
}
```

This is a relative path, from the EfcDataAccess component root. However, the program is started from WebAPI component. 
So, the path to the file should be relative to this component. Modify the above code to:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Todo.db");
}
```

The `..` means to navigate to a parent folder, which is TodoAppWasm solution folder. Then into the EfcDataAccess folder (component), and the the Todo.db file.

Alternatively, you can provide the absolute path, something like:

```
C:\TRMO\RiderProjects\TodoAppWasm\EfcDataAccess\Todo.db
```

However, that path will be different between group members, so you would have to modify it. Alternatively this can be put into a local configuration file, which is not under version control. You'll have to google how to do this.
