# The Authentication State
The previous slide showed how to get user information in the view part.

What if you need it in the code block?\
Maybe you are about to create a Product, and you wish to extract the user name to set as owner.\
Maybe one method does different things based on your Claims.

Whatever your reason or need, you can get access to the information.

Let's try an example. We will redo the previous page, it will look the same to the user, but the programming is different.

Create a new page, you can call it "ShowCredentialsFromAuthState", or something else.

It looks like this:

```razor
@page "/ShowCredentialsFromAuthState"
@using System.Security.Claims

<h3>Show Credentials 2</h3>
<p>Here we can see the users credentials</p>

@if (isLoggedIn)
{
    <h3>Hello @name</h3>
    <p>
        Your claims include:
        <ol>
            @foreach (Claim claim in userClaims!)
            {
                <li>
                    <label>@claim.Type: @claim.Value</label>
                </li>
            }
        </ol>
    </p>
}
else
{
    <p>You are not logged in.</p>
}

@code {

    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; } = null!;

    private string? name;
    private IEnumerable<Claim>? userClaims;
    private bool isLoggedIn;

    protected override async Task OnInitializedAsync()
    {
        AuthenticationState authState = await AuthState;
        ClaimsPrincipal user = authState.User;
        isLoggedIn = user.Identity != null;
        
        if (!isLoggedIn) return;
        
        userClaims = user.Claims;
        name = user.Identity!.Name!;
    }

}
```

We have a bit more going on here. The view part is less complex this time, though.

We have a boolean `isLoggedIn` and if it is true, we display the `name`and also all the claims of the user.

Otherwise we just display "You are not logged in".

So, no Authorize components or other stuff in the view to block/hide/show things.

##### Let's take a look at the code.

The first property is the `AuthenticationState`, i.e. logged in information. It is marked with `[CascadingParameter]`. This is because it is set from a parent component far away. Remember how we modified the App.razor to wrap everything in <CascadingAuthenticationState>? This component will set the AuthenticationState on all components/pages in your app, if they request it, like we do with this property.

We also have the property to contain the `name` of the user, and a collection of claims, and then the boolean to see if the user is logged in.

##### Now, the method `OnInitializedAsync()`.
Remember, this is automatically called whenever the page is loaded.\
First line, we await the AuthState, to receive the AuthenticationState. From that we pull out the ClaimsPrincipal in the next line.\
Then, if there is an Identity, it means the user is logged in.\
We can pull out the claims and name of the user.

In the previous slide we saw how to show something if the user fulfilled the MustBeVia policy.\
Now, we can in the code just pull out the Domain claim, and check if the value is "via", and then use a boolean to show/hide something in the UI.

So, we don't strictly need to define policies, but they make things a lot easier if the same rules apply to several parts of your app.