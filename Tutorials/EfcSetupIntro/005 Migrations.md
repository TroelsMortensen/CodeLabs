# What is a migration
Your domain model probably evolves over time, and your database should be kept in sync.

A migration is an update to the database, so whenever you change your domain model, you should create a migration. We will do this continuously throughout this tutorial.

Each migration is like a checkpoint in time, describing changes to the database.

## Add migration
A new migration is added through the terminal. You must be located in the same project as your DbContext. Then the command is:

```bash
dotnet ef migrations add <MigrationName>
```

The `<MigrationName>` is a name you choose, and it should be descriptive of the changes you have made. For example, if you add a new entity, you could name the migration `AddAuthorEntity`.


## Apply migration
When you have a migration, you probably want to apply it to the database, so the changes are made. This is done with the following command:

```bash
dotnet ef database update
```

This command will look at the *Snapshot.cs class, and the migrations, and figure out which migrations to apply, to get the database up to date.

This means, you may add multiple migrations, and then apply them all at once.