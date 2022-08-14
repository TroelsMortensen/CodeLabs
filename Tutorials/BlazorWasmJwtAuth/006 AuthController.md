# The AuthController
Create a new class in the Controllers directory, call it `AuthController`.

### Add Controller stuff
First, modify it to look like this:

```csharp
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
```

Import what is necessary. This now marks this class as an ApiController, so that it's picked up by the Web API.\
We specify the route to hit this controller, so that it becomes `https://localhost:7130/auth`.

### Constructor and fields
Next, add the following fields and a constructor:

```csharp
private readonly IConfiguration config;
private readonly IAuthService authService;

public AuthController(IConfiguration config, IAuthService authService)
{
    this.config = config;
    this.authService = authService;
}
```

Import what is necessary.

Now, when this Controller is created (which happens whenever a request is made), the controller receives an instance of `IConfiguration`, which is used to read the "appsettings.json", we modified earlier.

We also get an IAuthService injected, i.e. here we just depend on the interface, applying the Dependency Inversion Principle.

### Claims generation
We need a method which can take a User (our own custom object) and turn it in to a Collection of Claims, which the programs understand. 

```csharp
private List<Claim> GenerateClaims(User user)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("DisplayName", user.Name),
        new Claim("Email", user.Email),
        new Claim("Age", user.Age.ToString()),
        new Claim("Domain", user.Domain),
        new Claim("SecurityLevel", user.SecurityLevel.ToString())
    };
    return claims.ToList();
}
```
Notice the `JwtRegisteredClaimNames` class is from 

`using System.IdentityModel.Tokens.Jwt;`

Then import the other necessary things with quick fix.

The method takes a User, and creates an Array of Claims.\
The first three are JWT stuff, recommended to be included. They may not be strictly necessary.\
Then follows a Claim for each of the properties of our User object. 

In your own projects your User object may look different, have different properties, and so you need to modify the above method accordingly, so that all relevant properties are turned into claims.

### JWT generation
This method will generate a JWT to be returned to the caller trying to log in.

```csharp
private string GenerateJwt(User user)
{
    List<Claim> claims = GenerateClaims(user);
    
    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
    JwtHeader header = new JwtHeader(signIn);
    
    JwtPayload payload = new JwtPayload(
        config["Jwt:Issuer"],
        config["Jwt:Audience"],
        claims, 
        null,
        DateTime.UtcNow.AddMinutes(60));
    
    JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
    string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
    return serializedToken;
}
```

This method is somewhat black magic, but it will generate the JWT, given a User object.

The first line calls the previous method to get a List of Claims, i.e. our User converted to a list of key-value pairs containing the same information.

A JWT is signed by the server, so it cannot be tampered with. We do that in the next two lines. 

We create a JwtPayload containing the relevant information:
* The Issuer, i.e. the server
* The Audience, i.e. the Blazor app
* The claims
* null, whatever that is
* An expiration date/time, meaning this JWT is only valid for a certain time, in this case 60 minutes. You can put whatever you wish here

In the end the JWT is serialized into a string of seemingly random characters.

### Login endpoint
We need a last method, the endpoint to be accessed when making a login request.

It looks like this:

```csharp
[HttpPost, Route("login")]
public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
{
    try
    {
        User user = await authService.ValidateUser(userLoginDto.Username, userLoginDto.Password);
        string token = GenerateJwt(user);
    
        return Ok(token);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
```
Do the imports.

We mark the endpoint as a Post request, and put the Route("login") resulting in the URI to hit this endpoint:

`https://localhost:7130/auth/login`

The method takes a `UserLoginDto` object, i.e. the class we created containing Username and Password.

The user info is validated, and upon success the JWT is generated and then returned.

If an exception happens, we return BadRequest. This is not particularly fine-grained, as we might wish to return different types of status codes, based on what went wrong. This is generally a better approach, but outside the scope of this tutorial.\
We could create various types of exceptions, and catch these specific exceptions across multiple catch-clauses, and return more specific status codes.