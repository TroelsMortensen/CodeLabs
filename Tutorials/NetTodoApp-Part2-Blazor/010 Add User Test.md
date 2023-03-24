# Test

We should now have the functionality in place. We will add a bit of styling later, but let's test it first.

## Pre-flight Checks!

![](Resources/pre-flight-check.jpg)

This is where a lot of students will realize, they have messed up something.
You could skip below, and just test, but if stuff doesn't work, go through this list:

### 1 - Run Web API with https
When you run your Web API, Rider may allow you to run a version, which only uses http.
You will see in the console output that the server only listens to one address: http://localhost:1234

Sometimes the server attempts to redirect incoming requests to use http***s***.
And if this is not set up correctly, things go iffy.\
So, I suggest just running the Web API with https. When doing this, you should see in the console that the server now listens on two addresses, one with http, and one with https. And different ports.

This may cause other problems, like that your developer environment is not trusted. You will have to set this up.\
First: open a terminal, there's on in Rider, usually as a tab at the bottom/left. Or go "View->Tool Windows->Terminal".\
In here, type `dotnet dev-certs https --clean`. This may show a popup, where you click accept.\
Then you type into the terminal `dotnet dev-certs https --trust`, again maybe with a popup, which you accept.

When your server is not trusted, and you try to open swagger, your browser will tell you, it is fishy, and do you really want to continue.

Use the https address, that's generally better.

### 2 Verify controller names
An often problem I see is that you have a class in your Web API call UserController.\
Then on the Blazor side, you try to create a new User with:\
POST https://localhost:1234/users

Notice the last _s_. But your URI must match what the server expects, and we have generally defined our controllers to be reached based on their naming.\
So, if your controller is named UserController, the URI to reach that controller will be `.../user` , i.e. with the s. 

Also notice, your Controller's file name may not match the class name, so you have to open the file and look at the code `public class UserController..`.

### 3 Blazor HttpClient BaseUrl
When doing Blazor-wasm, by default there is a registered HttpClient as a service. With Blazor-Server, you may have to register it yourself.\
Either way, you must verify the BaseUrl is set to match that of your Web API, _not_ your Blazor-WASM, which is the initial setting.\
Use the https URI of your Web API, as mentioned above.

## Actual test
Run first your Web API. You can just close the Swagger page, which opens.

Run then the BlazorWASM. It should open a new tab in your browser.

In the address bar put "https://localhost:7205/CreateUser" to navigate to the page, we have just worked on (your port may be different).

You should then see your page:

![img.png](Resources/CreateUserPageView.png)

Type in something in the text-field and see how the button becomes enabled.

Try first a user name, you know exists. We should see an error message.

Then try a new user name, non-existing. This should work, and you should see a green success message.\
If you have no users, do the above in reverse order.