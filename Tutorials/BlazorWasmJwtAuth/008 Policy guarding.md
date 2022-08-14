# Guarding
If your program is running, stop it.

Now we need to test how we can apply policies to guard our endpoints in the controllers.

In the Controllers directory, create a new Controller, call it "TestController".

Modify it to look like this:

```csharp
[ApiController]
[Route("[controller]")]
[Authorize]
public class TestController : ControllerBase
{
    
}
```

It's similar to the AuthController, but now notice the extra attribute `[Authorize]`.

This means this Controller can only be interacted with, if the caller provides a valid JWT.\
It means by default all endpoints can only be called with a valid token.\
If we leave out the attribute, all endpoints can by default be called by anonymous callers, i.e. no valid token is needed. It's up to you to decide whether to use it or not. For this test, we include the attribute.

We will create a couple of dummy methods, which just returns "OK", but the point is we will guard these endpoints with policies and authentication.

## Authorized guard
The below method has nothing extra attached to it. Add it to the TestController:

```csharp
[HttpGet("authorized")]
public ActionResult GetAsAuthorized()
{
    return Ok("This was accepted as authorized");
}
```

This looks like a normal, very simple endpoint. We just return status code 200 - OK. With a message.

The HttpGet("authorized") just indicates the sub-route to this specific endpoint, e.g.: 

`https://localhost:7271/test/authorized`

Let's test this.

##### Blocked access

Run your Web API.

Open the WebApiTests.http file.

We can separate multiple requests with ###, so, expand the content of your file to be:

```http request
// log in as Troels
POST https://localhost:7271/auth/login
Content-Type: application/json

{ "Username" : "trmo", "Password" : "onetwo3FOUR" }

###

GET https://localhost:7271/test/authorized
```

Notice the new GET request at the bottom, and the "###" separating it from the one above.

With your Web API running, run the GET request.\
The result should be something like:

```
https://localhost:7271/test/authorized

HTTP/1.1 401 Unauthorized
Content-Length: 0
Date: Sun, 14 Aug 2022 12:07:45 GMT
Server: Kestrel
WWW-Authenticate: Bearer

<Response body is empty>


Response code: 401 (Unauthorized); Time: 76ms; Content length: 0 bytes
```

The bottom line says, we were not authorized to call this endpoint. That's because we did not provide a valid JWT.

##### Now with token
In your .http test file add a new request, remember to separate it with "###":

Now, first, execute the first login request, resulting in a JWT.

Copy that JWT, and modify your latest GET request to something like this:

```http request
GET https://localhost:7271/test/authorized
Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiJmODhhMDJiMS0wMTdjLTQzOTktYTc3Zi1kMTVlNTk5MDA1ZGYiLCJpYXQiOiIxNC8wOC8yMDIyIDEyOjExOjE2IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InRybW8iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJUZWFjaGVyIiwiRGlzcGxheU5hbWUiOiJUcm9lbHMgTW9ydGVuc2VuIiwiRW1haWwiOiJ0cm1vQHZpYS5kayIsIkFnZSI6IjM2IiwiRG9tYWluIjoidmlhIiwiU2VjdXJpdHlMZXZlbCI6IjQiLCJleHAiOjE2NjA0ODI2NzYsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUJsYXpvcldhc21DbGllbnQifQ.w3qJXGPEYi6MMKH-t03KzryBmT7b7OqGJ6iEePDJuE06SI5hH27PS36Bo6QDrq1b_ykX5S0qxAfyJheSw-EDUA
```

This is how we provide a JWT along with our request. Notice the token above will not work with you. You will have to log in, and use that token.

Run the last GET request again, with the token. You should get back:

```
https://localhost:7271/test/authorized

HTTP/1.1 200 OK
Content-Type: text/plain; charset=utf-8
Date: Sun, 14 Aug 2022 12:12:23 GMT
Server: Kestrel
Transfer-Encoding: chunked

This was accepted as authorized

Response code: 200 (OK); Time: 72ms; Content length: 31 bytes
```

So, providing a valid JWT gives us access.