# Final Touches
We have most of the code in place. We just need to bind it together. We have several places used constructor dependency injection, but we need to tell the framework is available for injection.\
We do that in the WebAPI/Program.cs file. Open it.

We need to register various services:

```csharp
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, UserFileDAO>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
```

See result [here](https://github.com/TroelsMortensen/WasmTodo/blob/002_AddUser/WebAPI/Program.cs).
