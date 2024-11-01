# Adding New Users

We will implement the client side of the features in the same order as on the server (that's the current plan, at least).

This means we start here:

> As a user of the system I can add a new User, so that Todos can be assigned to Users.

## Interface
Whether we start developing the feature with the HttpClient layer or the UI layer, they both depend on the interface in between. So, let's start there.

In HttpClients component, create a new directory: "ClientInterfaces".

Inside this directory, create a new interface: "IUserService".

We already have a DTO for creating Users, used on the server side: `UserCreationDto`. 
Let us use this as the argument, and the return type will be the resulting User.  We get a User back from the endpoint, so this seems fitting.

This results in the method:

```csharp
Task<User> Create(UserCreationDto dto);
```

Add the method to the interface, fix import errors.


We will do the implementation next, and finally the UI.

## The Implementation

Inside HttpClients component, create a new directory to house the implementations. Call the directory: "Implementations".

We need a new class here: "UserHttpClient". It should implement the interface from above. Initially the class looks like this:

```csharp
public class UserHttpClient : IUserService
{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public Task<User> Create(UserCreationDto dto)
    {
        throw new NotImplementedException();
    }
}
```

We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework. This is best practice, and will improve longtime performance of your app.

If you are curious, [Nick Chapsas has an elaborate video about performance](https://www.youtube.com/watch?v=Z6Y2adsMnAA).

#### The method

First, add `async` to the method signature, otherwise we cannot await anything in the method body. Then let's do the method body. It looks like this:

```csharp
public async Task<User> Create(UserCreationDto dto)
{
    HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
    string result = await response.Content.ReadAsStringAsync();
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception(result);
    }

    User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    })!;
    return user;
}
```

We use the client to make a POST request to "/users", sending the dto. The dto will be serialized to JSON, and wrapped in an appropriate StringContent object.

Now, "/users" is generally not enough. We know the URI should be "https://localhost:7093/Users". However, on the HttpClient you can set a "base url", which is the first part, and we then only need to provide the part of the URI after the port. This base URL was set in Program.cs, in slide 5.


Every request returns a response, whether we actually expect an object back or not. 
We know that this endpoint will either return an error message, or the created User. 
So, we read the content of the response.\
If the response is not a success code, i.e. an error code in the 400 or 500 range, 
we know the result content is the error message, and an exception is thrown with that message.\
The exception can then be caught in the page and a message can be shown to the user of the app.

It is _always_ important to give feedback to the user, in both sunny or rainy scenarios. If there is no feedback, they might try again, and create two users, or they may not be aware that the user was not created. User feeback is _**important**_.

If the status code is success, in this case we expect a "201 Created", we know the result is a User as JSON, and it is deserialized and returned.\
We supply the `JsonSerializer` with options to ignore casing, because the result from the Web API will be camelCase, but our model classes use PascalCase for the properties.\
At the end of the call, line 13, there is a null-suppressor: "!", i.e. the exclamation mark. This is because, the Deserialize method returns a nullable object, i.e. `User?`, but we just above checked if the request went well, so at this point we know _there is_ a User to be deserialized.


Most of our client methods will have a very similar structure.

## Register as service
We will register the UserHttpClient as a service in `Program.cs`, so that the dependency framework can inject an instance into our pages, when needed.

Open BlazorWASM/Program.cs. Put in the line:

```csharp
builder.Services.AddScoped<IUserService, UserHttpClient>();
```

Next up, let's do the page.