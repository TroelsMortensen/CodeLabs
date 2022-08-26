# The Web API Controller

Next up, we will do the layer above the Application layer, i.e. the Web API. This is the access point into the system. We need a Web API endpoint which the client can call to create a new User object.

### Clean up
We already have the WebAPI component. Currently there are two template classes, and you can just delete them: WeatherForecast and WeatherForecastController.

### User Controller
Inside the Controllers directory, create a new class: "UserController". 
This class will be responsible for everything User object related. Result [found here](https://github.com/TroelsMortensen/WasmTodo/blob/002_AddUser/WebAPI/Controllers/UsersController.cs).

This is the initial code:

```csharp
using Domain.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
}
```

We first declare two using statements, and then the namespace.

Then we have the attribute `[ApiController]`. This attribute marks this class as a Web API controller, so that the Web API framework will know about our class.

The next attribute `[Route("[controller]")]` specifies the sub-URI to access this controller class. 
With that "route template", the URI will be `localhost:port/users`. 
If we rename our UserController to something else, the path will be changed too.\
We can define our own path with fx `[Route("api/users")]`, and then the URI would be `localhost:port/api/users`.
It is up to you whether you just stick to the default name, or pick something else.

The class extends ControllerBase to get access to various utility methods.

Then a field variable, injected through the constructor, so we can get access to the application layer, i.e. the logic.

### The endpoint
We need a method for this.

It should take the relevant data, pass it on to the logic layer, and return the result back to the client.\
It looks like this:

```csharp
[HttpPost]
public async Task<ActionResult<User>> Create(UserCreationDto dto)
{
    try
    {
        User user = await userLogic.Create(dto);
        return Created($"/users/{user.Id}", user);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e.Message);
    }
}
```

First, in line 1, we mark the method as `[HttpPost]` to say that POST requests to `/users` should hit this endpoint.

The method is async, to support asynchronous work. The return type is as a consequence a Task. 
This Task contains an ActionResult with a User inside. The ActionResult is an HTTP response type, which contains various extra data, other than what we provide.\
It is just more information to the client, in case it is needed. It is good practice.

We take a `UserCreationDto` as the argument. This is given to the logic layer through `userLogic` in line 6.\
The resulting User is then returned, with the method `Created()`, which will create an ActionResult with status code 201, the new path to this specific User (the endpoint of which we haven't made yet, but probably will),
and finally the user object is also included. In our case the server only sets the ID. But in other cases, all kinds of data can be set or modified when creating an object, so generally it is polite to return the result, so the client/user can verify the result.

If anything goes wrong in the layers below, we return a status code 500. That is not very fine grained, but we do include the method.\
A better approach is to create different custom exceptions, and catch them to then return different status codes. Maybe a ValidationException is thrown when validating the user data in the logic layer. We can then return a status code 400 indicating it was the clients fault, instead of the server.\
See a [list of status codes](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes).

Custom exceptions should be placed in the Domain component. They are, however, outside the scope of this tutorial. For now. Perhaps later steps will improve things in the future.