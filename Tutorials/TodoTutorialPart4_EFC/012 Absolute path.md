# Absolute Path to .db
Run the WebAPI.

If you don't get errors, great. However:

Sometimes, you'll get an error with the message "no such table: Todos", or whatever table you're trying to interact with.\
This is a poor way which SQLite tries to tell you the path to the .db file is not correct.

In the TodoContext::OnConfiguring, we just put: "Data Source = Todo.db".\
That means the Todo.db is a relative path, and the file should be found relative to the project.\
However, the project actually running is the WebAPI, not EfcData, and so it cannot find the Todo.db in the WebAPI project.

You can do one of two things:
1) Copy the Todo.db file into the WebAPI project
2) Update the path in TodoContext::OnConfiguring to an absolute path

I don't think the db file has anything to do with the Web API, so I'm not a fan of option (1).

Instead, I suggest you update the TodoContext::OnConfiguring to specify the absolute path to the file. An absolute path is from the drive root, e.g. c:\Users\...

An easy way:

![](AbsolutePath.gif)

Notice in the end that I prefix the string for Data Source with @. This is to auto-escape backslashes. Alternatively, every backslash would need to be doubled: `\\`.
