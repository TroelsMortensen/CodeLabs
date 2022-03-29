# Add Todo Endpoint

Next up, we will create an endpoint, which takes a Todo object, to be stored by the DAO.

The method looks like this:

```csharp
[HttpPost]
public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
{
    try
    {
        Todo added = await todoHome.AddAsync(todo);
        return Created($"/todos/{added.Id}", added);
    }
    catch (Exception e)
    {
        return StatusCode(500, e.Message);
    }
}
```

This time, the method is marked with `[HttpPost]` to indicate that if a POST request is made to "/todos", it should hit this specific endpoint.

The method is async.

It returns an `ActionResult<Todo>`. Why return a Todo? Well, the Todo object received is unfinished, there is no ID created yet. This is done by the DAO class.
Sometimes more work may be done by the server, setting other values on the object to be created. The client may be interested in seeing the finished object.

That's why we return the created Todo object. We wrap it in the `Created()` method, which results in a status code "201: Created". The first argument is the URI to the newly created Todo. We don't have an endpoint to support this yet, but that will come.\
The second argument is the created Todo object, `added`.

Again, we handle exceptions thrown by the TodoDAO, if any. Again, we could have caught specific exceptions, and handled them differently. Maybe return different status codes, or more elaborate error messages, based on the problem. But for now, this will have to suffice.