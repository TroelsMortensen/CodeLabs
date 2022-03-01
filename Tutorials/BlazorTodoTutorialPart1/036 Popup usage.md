# Show popup when Todo is added successfully

We will edit the AddTodo.razor, so that the modal is shown when a Todo is added.

### Code
First, remember to add a using statement: `@using UIElements`.
This goes at the top of AddTodo.razor.

Then a few changes to the code block of AddTodo.razor:

```csharp
@code {
    private Todo newTodoItem = new Todo();
    private string errorLabel = String.Empty;
    
    private bool showModal;
    
    private async Task AddNewTodo()
    {
        errorLabel = "";
        try
        {
            await TodoHome.AddAsync(newTodoItem);
            showModal = true;
            newTodoItem = new Todo();
        }
        catch (Exception e)
        {
            errorLabel = e.Message;
        }
    }

    private void Proceed()
    {
        showModal = false;
        NavMgr.NavigateTo("/Todos");
    }
}
```

Previously, around line 13, we would navigate to the Todos overview, but that is now removed.\
In line 14 we reset the view, by creating a `new Todo` so that the `OwnerId` and `Title` is cleared.\
We have a new method in line 22, which will just disable the modal and navigate to the Todos overview.
This method should be called, when a button in the modal is clicked.

### View

Next, we need to add the modal to the view. It is a simple approach, where it is hardcoded in, and just shown if a boolean is true.\
This is slightly inconvenient because you must include the modal in every view, where it can be used.\
If you use is as an error message, instead of our usual `errorLabel`, the modal must be in almost every page.

Other libraries have smarter ways, where it is just a method call to a utility class.

However, we start out simple.

The view is just updated at the end, after the `<EditForm>`, so the below is a fragment of the view:

```razor
    @if (!string.IsNullOrEmpty(errorLabel))
    {
        <label>@errorLabel</label>
    }
    @if (showModal)
    {
        <Modal>
            <p>You have successfully added a new Todo item. You should be very proud of yourself</p>
            <button @onclick="@Proceed">Wonderful</button>
        </Modal>
    }
</div>
```

The first if-statement was already present. 
The second, `@if(showModal)` is new. 
Notice, there is an opening `<Modal>` tag, and a closing. In-between we can put html and razor-syntax. Whatever we put here, will be the content in the popup.
This html-content is passed to the Modal component as the `ChildContent`.

The Modal contains a paragraph with a message, and a button.
When the button is clicked, the method `Proceed` is called. This method was defined further above.

The result, when adding a Todo, is now:

![img.png](Resources/ModalExample.png)


If you click <kbd>Wonderful</kbd>, you are taken to the todos overview.

Try it out.