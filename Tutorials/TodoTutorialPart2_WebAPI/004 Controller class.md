# Add Controller

You may clean up the project first, be deleting the `WeatherforecastController` and `WeatherForecast` classes.

Create a new class, `TodosController`, in the Controllers directory. (If you expanded your Todo app with users, you'll need a `UsersController` as well)

Modify the class to look like this:

```csharp
[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    
}
```

The attribute `[ApiController]` marks it as a controller, so it will be picked up by the framework. 
Thereby, we can make calls to the endpoints in this class.

The `[Route]` specifies how to access this specific controller with a REST request.

We inherit from `ControllerBase` to get access to convenient methods.