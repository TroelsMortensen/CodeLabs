# Changing the Added Services

We wish to be able to test this right away, even before starting implementation of the methods.

To do this, we need to change which implementation of ITodoHome is being used by the Web API. This is handled in WebAPI.Program.cs.

Currently, we have these two lines of code:

```csharp
builder.Services.AddScoped<ITodoHome, TodoFileDAO>();
builder.Services.AddScoped<FileContext>();
```

But we need to change it to:

```csharp
builder.Services.AddScoped<ITodoHome, TodoSqliteDAO>();
builder.Services.AddDbContext<TodoContext>();
```

So, the Web API still depends on the ITodoHome interface, but the implementation behind is now the TodoSqliteDAO.\
We also need the TodoContext, but notice it is added as a service using a different method: `AddDbContext`.

[Why this different method?](https://stackoverflow.com/questions/42716771/service-addscoped-vs-service-adddbcontext)