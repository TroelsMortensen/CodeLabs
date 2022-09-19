# Adding a Navigation Menu Item

Currently, we have had to manually input the URI into the browser address bar. We need another item in the navigation menu.

Open BlazorWASM/Shared/NavMenu.razor.

Find this part in the file (ca lines 10 to 28):

```razor{3-7}
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
    </nav>
</div>
```

Here we have the three nav menu items, e.g. lines 3-7 defines a single nav menu item, the home button:

![](Resources/NavMenuItemHome.png)

We can just copy the div and nested content and adjust a little bit. Make it like this:

```razor{18-23}
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
            <NavLink class="nav-link" href="CreateUser">
                <span class="oi oi-plus" aria-hidden="true"></span> Create user
            </NavLink>
        </div>
    </nav>
</div>
```

The `href=..` says which page to open, it should match the sub-URI in your page directive in the CreateUser.razor file.

## Cleaning up

Initially, there are two demo pages: Counter and Fetch Data. We might as well delete them and remove the links.

Remove the following from NavMenu:

```razor{8-17}
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
            <NavLink class="nav-link" href="CreateUser">
                <span class="oi oi-plus" aria-hidden="true"></span> Create user
            </NavLink>
        </div>
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="ViewUsers">
                <span class="oi oi-list" aria-hidden="true"></span> View users
            </NavLink>
        </div>
    </nav>
</div>
```

Those are the two links to Counter and Fetch data, respectively.

Now, you can also delete the two files, Counter.razor and FetchData.razor, from the Pages directory.

Let us leave the Index.razor, so we have some kind of home page.
