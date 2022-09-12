# Register Services
Now all the layers are in place. We just need to register services in the WebAPI/Program.cs class.

Add these two lines:

```csharp
builder.Services.AddScoped<ITodoDao, TodoFileDao>();
builder.Services.AddScoped<ITodoLogic, TodoLogic>();
```

## Test
That should be everything for this feature.

Let's test it.

Run your app, make a request to create a new Todo through Swagger, Postman, or Rider's built in HTTP client.

After a success response, go and have a look in WebAPI/data.json. Mine looks like this:

```json{12-21}
{
  "Users": [
    {
      "Id": 1,
      "UserName": "Troels"
    },
    {
      "Id": 2,
      "UserName": "Jakob"
    }
  ],
  "Todos": [
    {
      "Id": 1,
      "Owner": {
        "Id": 1,
        "UserName": "Troels"
      },
      "Title": "Test todo",
      "IsCompleted": false
    }
  ]
}
```
The highlighted lines shows the collection of persisted Todos.

Notice how the Todo contains the same User data as a User found in the Users collection. If the User objects had more properties, this JSON storage approach would really not scale well.

Try to create a Todo with empty Title, or set the OwnerId to 0, to test the rainy scenario. 
It is always important to test that your system is robust and can handle abuse by the user. Users will always mistreat your system.

![](Resources/UserAbuse.gif)