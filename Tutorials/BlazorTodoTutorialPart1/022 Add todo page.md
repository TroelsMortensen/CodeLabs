# Add Todo page
The final class can be found [here](https://github.com/TroelsMortensen/BlazorTodoApp/blob/Part1/Blazor/Pages/AddTodo.razor) for reference.

We are going to need a new page for adding todos, so create a new page, similar to the Todos page previously. In the Pages folder. Call it AddTodo.

The result should be in that file should be

```razor
@page "/AddTodo"
<h3>AddTodo</h3>

@code {
    
}
```

Next, we wish to add a **Form**. Blazor has built in components for this, which will utilize the data validation attributes,
we just added to the Todo class.

Now, we update the AddTodo page. It should look like below:

```razor
@page "/AddTodo"
@using Domain.Models
@using Domain.Contracts
@inject ITodoHome TodoHome
@inject NavigationManager NavMgr

<h3>AddTodo</h3>


@code {
    private Todo newTodoItem = new Todo();
    private string errorLabel;
    
    private async Task AddNewTodo()
    {
        errorLabel = "";
        try
        {
            await TodoHome.AddAsync(newTodoItem);
        }
        catch (Exception e)
        {
            errorLabel = e.Message;
            return;
        }
        
        NavMgr.NavigateTo("/Todos");
    }
}
```

Let us go over it.

**Line 1**: Again, the page directive, how to navigate to this page using URI.

**Lines 2-3**: Using statements, importing Models and Contracts

**Line 4**: Injecting the ITodoHome.

**Line 5**: This one is new. We are injecting a `NavigationManager`. This class exists in Blazor. It's used to navigate to other URIs, i.e. open other pages. We'll use it below.

**Line 11**: This is the Todo item we will fill out with the user-inputted data.

**Line 12**: This string will hold any error messages from trying to save the Todo. The content will be used in an html-label later.

**Line 14**: The method, which should be called when the user clicks the button for creating a Todo. Note it is async

**Line 16**: We reset the errorLabel, so it is not displayed.

**Line 19**: The call to ITodoHome to add a new Todo. This method call is wrapped in a try-catch, in case something goes wrong.

**Lines 21-25**: Here we catch the exception, and update the errorLabel with the exception message. 
This is so that the user gets feedback in case of errors.
It's always a good approach to let the user know if something went wrong.
If something *does* go wrong, we `return` i.e. exit the method so that the rest is not executed.

**Line 27**: We ask the `NavigationManager` to open the Todos overview page.

