# Login buttons
We can't have the user type in manually in the search bar every time they wish to log in.

Let's make some buttons for this, and put them somewhere convenient.

Create a new Blazor Component in the Pages directory, call it `LoginButtons`.

### The code
This is where we start, leaving out the view part:

```csharp
@using BlazorWasm.Services
@namespace Login
@inject NavigationManager navMgr
@inject IAuthService authService

...

@code {

    private void Login()
    {
        navMgr.NavigateTo("/Login");
    }

    private async Task Logout()
    {
        await authService.LogoutAsync();
        navMgr.NavigateTo("/");
    }

}
```

Notice there is no page directive here, so this is not a page we can navigate to. It is instead a piece of UI to be used in other components or pages.

We inject a `NavigationManager` and the `IAuthService` at the top.

In the code, we have a method to navigate to the Login page, and a method to log out. This `Logout()` method will tell the IAuthService to clear the logged in user, and then tell the app to navigate to the home page.

### The view
In step 11 you were asked to download the login and logout icons, and put them in the wwwroot/img folder. We'll use these icons now.

Here's the view:

```razor
<AuthorizeView>
    <Authorized>
        <img class="icon" src="img/Logout.png" @onclick="Logout"/>
    </Authorized>
    <NotAuthorized>
        <img class="icon" src="img/Login.png" @onclick="Login"/>
    </NotAuthorized>
</AuthorizeView>
```

Everything is wrapped in the AuthorizeView component, so that we inside get access to the authentication state, i.e. is someone logged in or not?

If the user is Authorized, i.e. logged in, we show the Logout icon, and attach the Logout() method to the on-click event.

If the user is not Authorized, i.e. not logged in, we display the Login icon.

### Styling
Create a style behind, and add this styling:

```css
.icon {
    width: 30px;
    cursor: pointer;
}
```

### Adding the buttons to the UI
Finally, open the Shared/MainLayout.razor component. We want to add the buttons to the top bar of the app.

We do that by adding the LoginButtons component to the MainLayout, like so:

```razor{11}
@inherits LayoutComponentBase
@using Login

<div class="page">
    <div class="sidebar">
        <NavMenu/>
    </div>

    <main>
        <div class="top-row px-4">
            <LoginButtons/>
        </div>

        <article class="content px-4">
            @Body
        </article>
    </main>
</div>
```

There used to be a link to some "about". We don't need that.
