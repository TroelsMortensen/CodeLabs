# Blocking access to a page
If there are pages you don't want anonymous users to access, you can block them similarly to how we blocked an entire Web API Controller: with the Authorize attribute.

Let's try.

Create a new Blazor page, you can call it "MustBeLoggedInToView.razor", or whatever.

The content of the page looks like this:

```razor
@page "/MustBeLoggedInToView"
@attribute [Authorize]

<h3>Must Be Logged In To View</h3>

<p>You can only access this page, if you're logged in. Given that you're seeing this, you're logged in</p>
```

It's very simple, nothing much going on. There is no code block.

The thing to notice is the second line:

`@attribute [Authorize]`

This piece of code says that this page is only displayed when you are logged in.

Let's test this.

Open the Shared/NavMenu component. Add an extra nav menu item:

```razor{18-22}
<div class="@NavMenuCssClass" @onclick="ToggleNavMenu">
    <nav class="flex-column">
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                <span class="oi oi-home" aria-hidden="true"></span> Home
            </NavLink>
        </div>
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="counter">
                <span class="oi oi-plus" aria-hidden="true"></span> Counter
            </NavLink>
        </div>
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="fetchdata">
                <span class="oi oi-list-rich" aria-hidden="true"></span> Fetch data
            </NavLink>
        </div>
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="MustBeLoggedInToView">
                <span class="oi oi-list-rich" aria-hidden="true"></span> Must Be Logged In
            </NavLink>
        </div>
    </nav>
</div>
```

### Test
Run Web API and Blazor again. Without logging in, try to access the page.

You should see Gandalf blocking your way.

Now log in, and try again.

### Adding policy
Currently you can view the page if you are logged in. But you can also apply a policy, only granting access if the logged in user fulfills that policy.

It's done with this line instead:

`@attribute [Authorize("MustBeVia")]`

Now, the user must fulfill the MustBeVia policy. 

Test this by rerunning Web API and Blazor. First log in as Jakob:\
jknr/password

Try to access the page. You should be blocked by Gandalf.

Log out, and in again as\
trmo/onetwo3FOUR

and try the page. You should have access.