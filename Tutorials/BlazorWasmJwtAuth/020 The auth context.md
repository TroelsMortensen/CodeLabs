# The Authentication Context 

Sometimes you wish to display things to the user about their credentials, or based on their credentials.

When inside a <AuthorizeView> in the view part of a page, we get access to the `context`, a variable containing this information about the logged in user. It was mentioned before.

Let's create a page, which displays a user's Claims.

Create a new page, you could call it "ShowCredentialsFromContext".

It looks like this:

```razor
@page "/ShowCredentialsFromContext"
@using System.Security.Claims

<h3>Show Credentials 1</h3>
<p>Here we can see the users credentials</p>
<AuthorizeView>
    <Authorized>
        <h3>Hello @context.User.Identity.Name</h3>
        <p>@context.User.Claims.First(claim => claim.Type.Equals(ClaimTypes.Role)).Value</p>
        <p>
            Your claims include:
            <ol>
                @foreach (Claim claim in context.User.Claims)
                {
                    <li>
                        <label>@claim.Type: @claim.Value</label>
                    </li>
                }
            </ol>
        </p>
    </Authorized>
    <NotAuthorized>
        <p>You are not logged in.</p>
    </NotAuthorized>
</AuthorizeView>

<AuthorizeView Policy="MustBeVia">
    <p>You are VIA</p>
</AuthorizeView>
```

Similar to the Login page, we have the <AuthorizeView> and inside we have two sub-tags: <Authorized> and <NotAuthorized>.

The content of each is shown based on your logged in states, as mentioned before.

In line 8, we have this:

`<h3>Hello @context.User.Identity.Name</h3>`

We get access to the context, an `AuthenticationState`. It contains a `ClaimsPrincipal`, i.e. the `.User`. 
This then contains the `Identity`, and here we can get the `Name`.

Remember this `Name` was set with a Claim of a specific _Type_ in the Web API's AuthController:

`new Claim(ClaimTypes.Name, user.Username)`

Now look at line 9, here we access the Claims to find the Role of the user.

And further down we display all the Claims through the foreach-loop.

Remember to add it to the nav menu.

### Block view with policy
You can block an entire page, but you can also block just part of a page, based on a policy. You can see that at the bottom of the code block above.