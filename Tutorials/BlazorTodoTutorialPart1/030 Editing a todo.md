# Editing a Todo

We will add a new feature, so that we can edit existing Todos. 
The main take-away from this feature is how to pass arguments/information from one page to another.

The code is in a new branch, [here](https://github.com/TroelsMortensen/BlazorTodoApp/tree/5EditTodo)

### Edit page
First, we need a new page to edit the todo. It is going to look a lot like the page for adding Todos.

Create a new page, call it EditTodo.razor.

### DAO
We already have a method in the TodoFileDAO.cs about updating a Todo, but it currently only updates the `IsCompleted` and `OwnerId`.\
We need to be able to update the `Title` as well. Make the below changes:

```csharp{1, 6, 8}
public Task UpdateAsync(Todo todo)
{
    Todo toUpdate = fileContext.Todos.First(t => t.Id == todo.Id);
    toUpdate.IsCompleted = todo.IsCompleted;
    toUpdate.OwnerId = todo.OwnerId;
    toUpdate.Title = todo.Title;
    fileContext.SaveChanges();
    return Task.CompletedTask;
}
```
Notice a change in lines 1, 6 and 8.

1 - The `async` keyword was removed, as I have realized it wasn't necessary. It is not critical, just a bit of a waste. I will update the rest eventually. You can go ahead and change the other methods in TodoFileDAO similarly, as none of them are doing anything asynchronously.

6 - We now update the `Title`.

8 - We return `Task.CompletedTask`. We need to do this, because we removed the `async` keyword from the method header.
Later, when we add a database, we will actually make use the async, so the return type of Task remains.


### Code
The code of the page should look like this:

```razor
@page "/EditTodo/{Id:int}"
@using Domain.Models
@using Domain.Contracts

@inject ITodoHome todoHome
@inject NavigationManager navMgr

<h3>EditTodo</h3>

@code {

    [Parameter]
    public int Id { get; set; }

    private Todo todoToEdit;
    private string errorLabel = String.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        todoToEdit = await todoHome.GetByIdAsync(Id);
    }

    private async Task Save()
    {
        errorLabel = "";
        try
        {
            await todoHome.UpdateAsync(todoToEdit);
            navMgr.NavigateTo("/Todos");
        }
        catch (Exception e)
        {
            errorLabel = e.Message;
        }
    }
}
```
A few things to notice here.

**Line 1:** Notice the page directive looks different from what we have seen so far. We have added a _route parameter_: The `/{Id:int}`.
It means that we can open this page with a URI like "/EditTodo/7", which means we want to edit Todo with id 7.
The `:int` is a constraint saying to convert the parameter to `int`. Otherwise it will just be a string. 
Notice the property `Id` in the code block, it is marked with the attribute `[Parameter]`. That means the value can be set from the outside, e.g. with a route parameter.

**Lines 5-6:** We inject needed services.

**Lines 18-21:** When the page loads, we get the `Todo` by `Id`.

**Lines 23-35:** Here we save the changes. If the update goes through (i.e. no exceptions), we are taken back to the Todos overview.

### View
Next, the view part.

```razor
<div class="box">
    <h3>Edit Todo</h3>
    <EditForm Model="@todoToEdit" OnValidSubmit="@Save">
        <DataAnnotationsValidator/>
        <ValidationSummary/>
        <div class="form-group field">
            <span>
                <label>User Id:</label>
            </span>
            <span>
                <InputNumber @bind-Value="todoToEdit.OwnerId"/>
            </span>
        </div>
        <div class="form-group field">
            <span>
                <label>Title:</label>
            </span>
            <span>
                <InputTextArea rows="4" @bind-Value="todoToEdit.Title"/>
            </span>
        </div>
        <p class="actions">
            <button class="acceptbtn" type="submit">Update</button>
        </p>
    </EditForm>
    @if (!string.IsNullOrEmpty(errorLabel))
    {
        <label>@errorLabel</label>
    }
</div>
```
You may notice heavy similarities to the AddTodo page. It was essentially copied, and a few variables needed changing.


### Styling
You can make a style-behind and just copy everything from the `AddTodo.razor.css`.

Alternatively, since we are now using the same styling multiple place, it would indicate that if the style changes, we might want it to change for both AddTodo and EditTodo.

This would require the styling to be put a shared place, like the wwwroot/css/site.css style sheet.

If you do move the css here, you should not have duplicate css in your style behinds.

### Test

If you run your app, you can manually type in a URI like: `https://localhost:7140/EditTodo/1`. 

Next slide will update the Todos over view, so we can click an edit button.

