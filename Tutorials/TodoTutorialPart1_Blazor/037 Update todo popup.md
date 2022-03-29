# Update todo in modal

## Still working on this part...

Having to navigate to a new page just to update an existing todo may feel unnecessary.\
Instead, we can use our new modal component to show a popup in which we can edit the todo data.

The result is found in [this branch]() (eventually).

### Code block
First, we need a few modifications to the `Todos.razor`.
We are going to use our new modal here to create a popup, where we can update a Todo's information.

Include the following in the code block:
```csharp
private bool showModal;
private string editedTitle = String.Empty;
private int editedOwner;
private Todo todoToEdit = new();
private string editErrorLabel = string.Empty;

private void ShowEdit(Todo todo)
{
    todoToEdit = todo;
    editedOwner = todo.OwnerId;
    editedTitle = todo.Title;
    showModal = true;
}

private async Task AcceptEdit()
{
    try
    {
        Todo updated = new()
        {
            Id = todoToEdit.Id,
            Title = editedTitle,
            IsCompleted = todoToEdit.IsCompleted,
            OwnerId = editedOwner
        };
        await TodoHome.UpdateAsync(updated);
        todoToEdit.Title = updated.Title;
        todoToEdit.OwnerId = updated.OwnerId;
        showModal = false;
    }
    catch (Exception e)
    {
        editErrorLabel = e.Message;
    }
}
```

We have first a couple of new field variables.
* A bool to manage whether to show the modal
* A string to hold the new title of the Todo
* An int to hold the new owner
* A reference to the Todo being edited
* And an error string to display error messages

Next a method, `ShowEdit()`. This method is called when you click the edit button in the table (well, not yet, but soon).
We assign various variables, and show the modal.

The next method, `AcceptEdit()`, is called when the accept button is clicked in the modal.\
We create a new Todo, with information from the existing Todo-being-edited, and the new information, i.e. `Title` and `OwnerId`.\
Then, that new Todo is sent to the layer below to be persisted.\
When the method call `TodoHome.UpdateAsync()` succeeds, the updated todo is persisted, and we need to update the view as well, to reflect the changes.\
The `todoToEdit` is a reference to the Todo object, currently held in a list, here in this view. When we modify its data, that is reflected in the view.\
So, we update `Title` and `OwnerId`.\
Then, the `showModal` bool is set to false, to hide the Modal again.

Now, we just need to update the view.

### View

At the end of your current view, we will insert the new modal code.
It doesn't really matter where, we put it. It just cannot be nested in other HTML.

See below. The first if-statement already exists in your current view, I just include it to show where I have put the Modal. 
I.e. at the end of the view.

```razor
@if (!string.IsNullOrEmpty(errorLabel))
{
    <label style="color: red">@errorLabel</label>
}

@if (showModal)
{
    <Modal>
        <h3>Edit Category</h3>
        <hr/>
        <div style="margin-bottom: 5px">
            <label style="font-weight: bold">Todo Title</label>
        </div>
        <div style="margin-bottom: 5px">
            <textarea style="width: 20ch; border-radius: 10px; padding: 3px;" @bind="@editedTitle"/>
        </div>
        <div style="margin-bottom: 5px">
            <label style="font-weight: bold">Owner</label>
        </div>
        <div style="margin-bottom: 5px">
            <input type="number" min="0" style="width: 10ch; border-radius: 10px; padding: 3px; text-align: center" @bind="@editedOwner"/>
        </div>
        @if (!string.IsNullOrEmpty(editErrorLabel))
        {
            <div>
                <label style="color:red">@editErrorLabel</label>
            </div>
        }
        <div style="margin-top: 15px">
            <button style="margin-right: 10px" @onclick="AcceptEdit">Accept</button>
            <button style="margin-left: 10px" @onclick="@(() => showModal = false)">Cancel</button>
        </div>
    </Modal>
}

```
 Now, everything inside the if-statement `@if (showModal)`, this is where the new interesting stuff happens.\
 We are again using the `<Modal>` component, and inside you see some HTML, which will be the `ChildContent`, i.e. the HTML shown in the popup.
 
Bunch of `<div>`s to organize things. There's a label and below, a `<textarea>` with its value bound to `editedTitle`.\
Further down, another label and a number-input, with the value bound to `editedOwner`.

Then the usual errorLabel, in case any errors needs to be shown.

And the last `<div>` holds two buttons. The first will call `AcceptEdit`, the other will just flip the bool `showModal` to hide the Modal again, if needed. I.e. if the user cancels the edit.

The last thing we need, is so that the edit button in the view now opens the Modal instead of another page.

Here is the updated piece of code, notice the method call `ShowEdit`, instead of the previous `Edit`. Also, another argument is used.

```razor
<td>
    <img src="img/edit.gif" class="funnel" @onclick="@(() => ShowEdit(item))"/>
</td>
```

Now you should be able to run your app, go to the Todos overview, and click any edit button. You should see the modal, you can change the Todo information, and click accept, and the changes should be reflected in the view right away.