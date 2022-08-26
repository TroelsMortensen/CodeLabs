# Web API Endpoint for Retrieval of Todos
Open TodosController.cs.

We need a new method so the client can request todos.

* It is a GET endpoint
* It needs arguments matching the search parameters.
* We need to handle exceptions
* The search parameters are wrapped into the DTO and forwarded to the Logic layer

Give it a go. Then look at the hint below for my solution and some comments.


<details>
<summary>hint</summary>

Did you remember to make all arguments **nullable**? Otherwise they will get a default value, e.g. the "completed status" will be set to false. That is not the intended behaviour. 

Did you remember to mark the parameters with `[FromQuery]`?

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<Todo>>> Get([FromQuery] string? userName, [FromQuery] int? userId,
    [FromQuery] bool? completedStatus, [FromQuery] string titleContains)
{
    try
    {
        SearchTodoParametersDto sp = new(userName, userId, completedStatus, titleContains);
        var todos = await todoLogic.Get(sp);
        return Ok(todos);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e.Message);
    }
}
```

Now, if we have many search parameters, the number of method-arguments is going to be fairly big, and that's somewhat inconvenient.\
The problem is that with GET requests, we cannot include an object, like when we do POST requests. Otherwise we could just have the client create a `SearchTodoParametersDto` object and send that along.\
We could abuse the HTTP request types and make this a POST, but there is a good reason to stick to the convention, it's easier for clients/users to understand how to use our Web API. We should try to stick to the convention.\

We could make a kind of hack, where the query parameter of the URI could contain a json object, and on the server side, we would deserialize that. But I'm not convinced I like that approach.

So, for now we have to accept the large number of arguments. Maybe I will stumble upon a better approach later.

</details>

## Test

You should be able to test this endpoint now. Try with different filters.