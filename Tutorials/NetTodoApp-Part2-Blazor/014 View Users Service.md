# User Service Method

We start by implementing a method to retrieve all users. 

In IUserService interface we need the following method. The Web API actually allows us to search for Users by user name, so lets include that.

```csharp
Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
```

By assigning the argument to null here, we can provide a default value. This means we do not need to provide an argument when calling the method, and if we don't, the argument will be set to null. Alternatively, we would have to actively pass `null` as the argument, when calling the method from a page's code block.

## Implementation
Let's implement the method in UserHttpClient:

```csharp
public async Task<IEnumerable<User>> GetUsers(string? usernameContains = null)
{
    string uri = "/users";
    if (!string.IsNullOrEmpty(usernameContains))
    {
        uri += $"?username={usernameContains}";
    }
    HttpResponseMessage response = await client.GetAsync(uri);
    string result = await response.Content.ReadAsStringAsync();
    if (!response.IsSuccessStatusCode)
    {
        throw new Exception(result);
    }

    IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result)!;
    return users;
}
```

The method is async, because we make a call to the Web API, which may take time. The argument is again defaulted to null. And the return type is `IEnumerable<Users>`, i.e. the immutable collection returned from the Web API endpoint.

First the sub-URI is defined to be "/users".\
Then, if the method-argument is not null, we suffix that to the URI as a query parameter. The URI might then e.g. be: "/users?username=roe", to fetch all users whose name contains "roe".

Next follows template code very similar to the other method in the class. A GET request is made, the response is checked for success. In case of failure, an error is thrown. In case of success, the string is de-serialized from JSON to `IEnumerable<User>`, which is then returned.

That should be all for the service.