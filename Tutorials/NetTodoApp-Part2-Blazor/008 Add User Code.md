# The Code
Let's start with the code block, i.e. what will essentially be your view-model. My style is typically to have the code block inside the page, as shown below. But if you prefer to do code-behinds, you're welcome to do that.

We need to hold the data, that the user inputs. And we need a method to send that data to the UserService.

It looks like this:

```csharp
@code {
    private string username = "";
    private string resultMsg = "";
    private string color = "";
    
    private async Task Create()
    {
        resultMsg = "";

        try
        {
            await userService.Create(new UserCreationDto(username));
            username = "";
            resultMsg = "User successfully created";
            color = "green";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            resultMsg = e.Message;
            color = "red";
        }
    }
}
```

Three field variables. The `resultMsg` is to hold any messages, we wish to display to the user. Maybe something fails on the server side, e.g. the user name was already taken. We wish to display that information to the user. Remember, feedback is important.\
The `color` is to set the color of the resultMsg in the view, shown on the next slide.

The `Create()` method does very little. We reset the `resultMsg` first. Then a call to the `IUserService` with a new UserCreationDto containing the user name.

If something goes wrong, we catch any potential exceptions, and display their message, by assigning the exception message to the `resultMsg` variable, which causes an update to a field variable used in the view, which will then be re-rendered.
