# View Users View

So, the view part of viewing users. The HTML and razor-syntax (i.e. inline C# code).

We wish to display all user names. That can be formatted in different ways. 
It could just be a bullet list, or something else. 
I have again attempted to try some attempt of maybe applying some kind of fancy styling, sort of. 
We'll take that on the next slide.

Here is the html razor-syntax:

```csharp
@page "/ViewUsers"
@using Domain.Models
@using HttpClients.ClientInterfaces
@inject IUserService userService

<h3 style="text-align: center">Overview of all users</h3>

@if (!string.IsNullOrEmpty(msg))
{
    <div>
        <span>@msg</span>
    </div>
}

@if (users == null)
{
    <span>Loading..</span>
}
else if (!users.Any())
{
    <span>No users to be found</span>
}
else
{
    <div class="users-container">
        @foreach (User user in users)
        {
            <div class="user-card">
                <label>@user.UserName</label>
            </div>
        }
    </div>
}
```

**Line 1**: The page directive, i.e. the URI to access this page.

**Lines 2-3**: Import statements, so that we can access classes `User` and `IUserService`.

**Line 4**: Here we inject an `IUserService` instance.

**Line 6**: It's just a header. I have committed crimes and just inlined a bit of styling in the tag.

**Line 8**: Here we display the `msg` in case it has any value. This happens in case of errors.

**Line 15**: Here starts an if-else if-else:

**if**: the `users` variable is null, it is because no data has been retrieved from the server.

**else if**: this is the case, when the `users` are loaded, but the collection is empty. The method `Any()` returns true, if there are any elements in the collection.

**else**: finally, here we have loaded data, and there is actually some data. We wish to display all users. There is a `foreach` to loop through the `users`, and for each User object, we generate a little piece of html, i.e. the `<div` and `<label>` tags.\ 
This piece here: `<label>@user.UserName</label>`, notice how we use the `user` variable from the foreach-loop, and get the `UserName`. This is a string, which will then be displayed in the UI.

Again, notice how `@` is used to inline some C# code, which is evaluated to generate the resulting HTML. We can inline C# just about anywhere, so the dynamic HTML generation can be _very_ flexible.

Finally, let's add some styling.