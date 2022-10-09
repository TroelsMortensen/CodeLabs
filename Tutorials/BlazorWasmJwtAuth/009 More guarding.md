# More policy guarding examples
Currently, we have blocked our controller so that you need a valid JWT to call the endpoints.

## Allow anonymous
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

Test that you can reach this endpoint without including the JWT.

## Guard with policy
Now, we can have open endpoints and endpoints requiring a valid token, i.e. you are logged in. But what about applying a specific policy, i.e. you must be _x_ or have _y_ or whatever.

Try the following endpoint in your TestController:

```csharp
[HttpGet("mustbevia"), Authorize("MustBeVia")]
public ActionResult GetAsVia()
{
    return Ok("This was accepted as via domain");
}
```

Here we use the `Authorize("MustBeVia")` attribute. Remember, in the Shared/Auth/AuthorizationPolicies.cs class we defined a policy named "MustBeVia".

You have two users hardcoded: "trmo", and "jknr". Only "trmo" is has the domain "via".\
So, first log in with "trmo", receive the token, and use that token with a GET request to `https://localhost:7271/test/mustbevia` .

It should be okay.

Then try to modify the login credentials to "jknr" and "password", to log in as Jakob. Copy the received token into your request above, and try again. You should get an Unauthorized response back.

## Manual checking

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
We access the `User` property. This has little to do with your specific User object.
This property comes from the superclass, `ControllerBase`, and is the same type of ClaimsPrincipal we used in the AuthorizationPolicies.cs class. I.e. it contains information about the user stored in the provided JWT from the request.

You can extract various information from this `User`, e.g. the claims.

## Auth Layer Responsibility
You may spend a little while considering which layer should be responsible for authentication and authorization.

When we apply the attributes, we let the Web API layer be responsible for auth/auth. But what if we swap out this layer, with something else, where auth is not built in? E.g. basic sockets, or maybe a gRPC server (don't know if it has built in auth). 
You will have to manually implement auth then. And if you swap out once again, you must redo all the auth.\
We have a layer, the Web API, which has two different responsibilities: receiving requests, and auth. This implies we should extract auth-related responsibilities to somewhere else.\
Assuming we have the usual three layers of network, logic, and data access, is auth then part of the logic layer? Well, maybe. It could be, but it still feels like something else than business logic.\
We could introduce a new layer between network and logic, with the only responsibility of protecting the logic layer. We would still have to extract the user information from the Request, and should then pass that along to the lower layers.

It does potentially get a little elaborate, and we probably are not going to swap out the networking all the time.

This discussion is just to get you thinking.