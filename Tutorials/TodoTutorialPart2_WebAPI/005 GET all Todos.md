# GET All Endpoint

First, we will create the method, which can return all Todos. This is used for the view in the Blazor App, which shows a table of all Todos.

Eventually, we will need an Endpoint for all methods in the `ITodoHome` interface.

## Constructor

First, we need a constructor for `TodosController`, so that we can inject the data service:

```csharp
private ITodoHome todoHome;

public TodosController(ITodoHome todoHome)
{
    this.todoHome = todoHome;
}
```

## GET
In TodosController create a new method:

```csharp
[HttpGet]
public async Task<ActionResult<ICollection<Todo>>> GetAll()
{
    try
    {
        ICollection<Todo> todos = await todoHome.GetAsync();
        return Ok(todos);
    }
    catch (Exception e)
    {
        return StatusCode(500, e.Message);
    }
}
```

We mark the method with `[HtppGet]` to indicate that if a GET request is made to `/todos` it must hit this endpoint.

The method is `async`. 

The return type is `ActionResult`, which returns an http response with an `ICollection<Todo>`.

The method just uses the `todoHome` to fetch all todos, and return them. That is done with `Ok(..)`, which will be a status code of 200. This convenience method comes from `ControllerBase`. There are other similar methods, e.g. `BadRequest`, or `NotFound`.

If something were to fail on the server side, we need to provide information of that to the client. Therefore, we have the try-catch. 
In case of an error, we currently just return an ActionResult with status code 500, to indicate server-error, and the message of the exception. We could add multiple catch clauses, if we wanted to be more specific about which errors returns which result. Different things can go wrong, and you may want to handle that with different approaches. For now, however, this will suffice.