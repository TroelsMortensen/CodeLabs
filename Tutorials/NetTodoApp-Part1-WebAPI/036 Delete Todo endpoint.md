# Delete Todo Web API Endpoint

Finally, we need the endpoint for the client to call.

* It is an HttpDelete method
* It returns nothing, i.e. Task<ActionResult>
* The argument is an int, the ID, taken from the route

Give it a go.

<details>
<summary>hint</summary>

```csharp
[HttpDelete("{id:int}")]
public async Task<ActionResult> DeleteAsync([FromRoute] int id)
{
    try
    {
        await todoLogic.DeleteAsync(id);
        return Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e.Message);
    }
}
```

The [HttpDelete("{id:int}")] marks this method for DELETE requests. The part in the parenthesis is the sub-uri, indicating you here put an id of type int.

A URI could be: DELETE /todos/7

This would then request to delete Todo with id 7.

The id sub-part of the URI is passed as the argument, because the argument is marked [FromRoute], meaning this argument is found in the URI.
The argument variable must be named the same as in the URI template, i.e. "id".

</details>