# Wrap in Auth
We need to make available authentication across our app.

Open BlazorWasm/App.razor. Modify it to look as follows:

```html{1,4-9,19}
<CascadingAuthenticationState>
    <Router AppAssembly="@typeof(App).Assembly">
        <Found Context="routeData">
            <AuthorizeRouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)">
                <NotAuthorized>
                    <img width="600" src="https://i.kym-cdn.com/entries/icons/original/000/002/144/You_Shall_Not_Pass!_0-1_screenshot.jpg"/>
                    <p>You'll have to log in!</p>
                </NotAuthorized>
            </AuthorizeRouteView>
            <FocusOnNavigate RouteData="@routeData" Selector="h1"/>
        </Found>
        <NotFound>
            <PageTitle>Not found</PageTitle>
            <LayoutView Layout="@typeof(MainLayout)">
                <p role="alert">Sorry, there's nothing at this address.</p>
            </LayoutView>
        </NotFound>
    </Router>
</CascadingAuthenticationState>
```

The yellow highlighted lines contains the changes. We wrap everything in <CascadingAuthenticationState>. And the <RouteView> is changed to <AuthorizeRouteView>.

Everything inside the <NotAuthorized> tags is html to be shown, if the user tries to access something, they are not authorized to access.

### Imports
You should now see that the tag <CascadingAuthenticationState> is a different color than e.g. <Router...>.

That's because C# recognizes the second tag as a razor component, and the first tag is not currently recognized, so it's just colored as html.

We need an import statement.

Open the file BlazorWasm/_imports.razor. At the end, add the following two using statements:

```csharp
@using Microsoft.AspNetCore.Components.Authorization
@using Microsoft.AspNetCore.Authorization
```

Go back to App.razor, and verify the color change, indicating <CascadingAuthenticationState> is now recognized as a component. The result is that in every page or component, we can request information about the logged in user. That is convenient.