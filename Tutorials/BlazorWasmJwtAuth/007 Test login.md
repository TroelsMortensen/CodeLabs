# Testing the login
It's time to test whether we can log in.

Normally, when you run your Web API, it opens a Swagger page. You can test the Login endpoint from here, and if you provide valid credentials, you should get a JWT back.

However, future calls which requires the JWT attached are more complicated to create with Swagger. We would have to add stuff in the Program.cs, and complicate things.

So, we'll do something else. We could use Postman, that would be easy enough, and if you prefer that, go for it.

### The .http test file
Rider has a built in Http Client. It's pretty simple.

In the WebApi project folder, create a new file. Call it "WebApiTests.http", or whatever, but the file type is ".http":

![img.png](Resources/AddHttpFile.png)

And here's the file:

![img.png](Resources/AfterAddingHttp.png)

Open the file. In here we can define calls to our Web API.

Input the following:

```http
// log in as Troels
POST https://localhost:7130/auth/login
Content-Type: application/json

{ "Username" : "trmo", "Password" : "onetwo3FOUR" }
```

The // marks a comment.

Then we define a "POST" request to "https://localhost:7130/auth/login".

We define the Content-Type to be "application/json", i.e. saying the request contains content in the format of JSON.

Finally a JSON object matching the format of our `UserLoginDto`.

Run the Web API, and then run your test. You should receive a JWT.

![](Resources/LoginRequest.gif)

In my test I received the following JWT:

"eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZ
W4iLCJqdGkiOiI3MzFkNTk2MS0zYjU1LTQxMz
UtYjFkOS03MTlhMjI5NDYyY2MiLCJpYXQiOiI
xNC8wOC8yMDIyIDExOjUyOjIwIiwiaHR0cDov
L3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwN
S8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6In
RybW8iLCJodHRwOi8vc2NoZW1hcy5taWNyb3N
vZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkv
Y2xhaW1zL3JvbGUiOiJUZWFjaGVyIiwiRGlzc
GxheU5hbWUiOiJUcm9lbHMgTW9ydGVuc2VuIi
wiRW1haWwiOiJ0cm1vQHZpYS5kayIsIkFnZSI
6IjM2IiwiRG9tYWluIjoidmlhIiwiU2VjdXJp
dHlMZXZlbCI6IjQiLCJleHAiOjE2NjA0ODE1N
DAsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2
VydmVyIiwiYXVkIjoiSldUU2VydmljZUJsYXp
vcldhc21DbGllbnQifQ.A3ncVJsEcfYD5AArX
R3F1tKYr7QaL7HQ_ln-kwWZbwXpx2aQz08DgV
X2l8CnfZr7FxE8BzsrO7-A1Mru4Gklbg"

If you are interested, you can go to [jwt.io](https://jwt.io/) to unpack the token:

![img.png](Resources/UnpackedToken.png)
