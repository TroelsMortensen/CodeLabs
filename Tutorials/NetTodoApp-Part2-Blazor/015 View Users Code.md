# View Users Code Block

Next up, let's do the code block in the page.

When it loads, we need to retrieve the list of users, so that it can be displayed. Here we go:

```csharp
@code {
    private IEnumerable<User>? users;
    private string msg = "";

    protected override async Task OnInitializedAsync()
    {
        msg = "";
        try
        {
            users = await userService.GetUsers();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            msg = e.Message;
        }
    }
}
```

We have first a field to hold the users. It is marked as nullable, because it will be `null` until the data is retrieved from the server.

Then a field to hold any messages, in case of errors.

The method is overridden from ComponentBase. All blazor components automatically inherits from it.\
This specific method is automatically called, whenever the page loads.\
It just retrieves a collection of Users from through the IUserService. In case of errors, the message is assigned to `msg`, which can then be displayed in the view.

Remember, if the IUserService implementation receives a failure status code from the Web API, an exception is thrown. That's what we catch here.