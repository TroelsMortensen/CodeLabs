# More policy guarding examples
Currently, we have blocked our controller so that you need a valid JWT to call the endpoints.

#### Allow anonymous
Maybe we wish to open some of the endpoints to everyone, we can do that with the "AllowAnonymous" attribute:

```csharp
[HttpGet("allowanon"), AllowAnonymous]
public ActionResult GetAsAnon()
{
    return Ok("This was accepted as anonymous");
}
```

Create another test request in your .http file to the endpoint `https://localhost:7271/test/allowanon` .\
Notice your port may be different.

#### Guard with policy
Now we can have open endpoints and endpoints requiring a valid token, i.e. you are logged in. But what about applying a specific policy, i.e. you must be _x_ or have _y_ or whatever.

Try the following endpoint in your TestController:

```csharp
[HttpGet("mustbevia"), Authorize("MustBeVia")]
public ActionResult GetAsVia()
{
    return Ok("This was accepted as via domain");
}
```

Here we use the `Authorized("MustBeVia")` attribute. Remember, in the Shared/Auth/AuthorizationPolicies.cs class we defined a policy named "MustBeVia".

You have two users hardcoded: trmo, and jknr. Only trmo is has the domain "via".\
So, first log in with trmo, receive the token, and use that token with a GET request to `https://localhost:7271/test/mustbevia` .

It should be okay.

Then try to modify the login credentials to "jknr" and "password", to log in as Jakob. Copy the received token into your request above, and try again. You should get an Unauthorized response back.

#### Manual checking

Finally, maybe you don't like to rely on policies. You can leave them out entirely if you wish to do so, and just do the authorization validation manually, i.e. writing that code yourself.

Here's an example endpoint:

```csharp
[HttpGet("manualcheck")]
public ActionResult GetWithManualCheck()
{
    Claim? claim = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role));
    if (claim == null)
    {
        return StatusCode(403, "You have no role");
    }

    if (!claim.Value.Equals("Teacher"))
    {
        return StatusCode(403, "You are not a teacher");
    }

    return Ok("You are a teacher, you may proceed");
}
```

Notice there is no Authorize attribute above the method signature.

In the method body, the first line:\
We access the `User` property. This has little to do with your specific User object. This property comes from the superclass, ControllerBase, and is the same type of ClaimsPrincipal we used in the AuthorizationPolicies.cs class. I.e. it contains information about the user stored in the provided JWT from the request.

You can extract various information from this `User`, e.g. the claims.