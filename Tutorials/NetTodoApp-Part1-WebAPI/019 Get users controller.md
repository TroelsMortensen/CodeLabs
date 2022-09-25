# The Controller Endpoint

With the Logic layer and the Data Access layer in place, we just need to create an endpoint in the UsersController, so that a client can request the data.

It looks like this:

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
{
    try
    {
        SearchUserParametersDto parameters = new(username);
        IEnumerable<User> users = await userLogic.GetAsync(parameters);
        return Ok(users);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e.Message);
    }
}
```

We mark the method with `[HttpGet]` so that GET requests to this controller ends here.

The return value is the `IEnumerable<User>` wrapped in an HTTP response message.

The argument is marked as `[FromQuery]` to indicate that this argument should be extracted from the query parameters of the URI.
The argument is of type `string?` indicating that it can be left out, i.e. be `null`.

A URI could look like:

`https://localhost:7093/Users?username=roe`

Indicating that we wish to filter the result by the user names which contains the text "roe".

Or if we want all users, we would use the URI:

`https://localhost:7093/Users`

If we later added other search parameters, e.g. age, we could have a URI like:

`https://localhost:7093/Users?username=roe&age=25`

Which would result in all users where the user name contains "roe" and their age is 25.

### Test
You should now be able to test your Web API. You could first put in a couple of users, and then try various filter texts and verify the result.