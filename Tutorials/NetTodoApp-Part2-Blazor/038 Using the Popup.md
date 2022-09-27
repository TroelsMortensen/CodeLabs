# Using the Popup

Now, let us put the popup into action. As mentioned, we wish to display a message, when a Todo is succesfully added.

Open "CreateTodo.razor".

We will have to modify various places.

[Code found in this branch](https://github.com/TroelsMortensen/WasmTodo/tree/018_PopupSuccessMessage)

## The View

We need to inject a `NavigationManager`, and import the UIComponents namespace, at the top:

```razor
@using UIComponents
@inject NavigationManager navMgr
```

Then we need to insert the Modal somewhere, you can just put it at the bottom of the view part:

```razor
<Modal ShowModal="showModal">
    <p>You have successfully added a new Todo item. You should be very proud of yourself!</p>
    <button @onclick="@Proceed">Wonderful</button>
</Modal>
```

So, the content of the popup is just a short message, and button. When the button is clicked, the user is taken to the View Todos page. So, we need to implement this method.


## The Code

We will need a boolean field variable to manage whether the popup is displayed.

And we will need a method to call. The functionality of this method is just to navigate to the View Todos page.

Add this to the code block:

```csharp
private bool showModal;

private void Proceed()
{
    showModal = false;
    navMgr.NavigateTo("/ViewTodos");
}
```

In the method `Create`, after the Todo has been created, we reset the view:

```csharp
selectedUserId = null;
todoTitle = "";
msg = "Todo created";
```

This is no longer needed, as the user is taken away from the page. You may remove this.

Instead, we must set the `showModal` to `true`, so they try block looks like this:
```csharp
try
{
    TodoCreationDto dto = new((int)selectedUserId, todoTitle);
    await todoService.CreateAsync(dto);
    showModal = true;
}
```

Notice how the modal is displayed after the `CreateAsync(..)` returns, meaning the request was a success.\
If the request fails, and exception will be thrown from Todo Service, and the `showModal = true` is skipped.

## Test

We should be ready test this new fancy feature.

Run the usual stuff.

Navigate to the Create Todo page.

