# Removing a Todo

Next up, we want to be able to delete a Todo. You have a todo about doing the dishes, but don't want to? Just delete it.

For this part, we will modify the existing `Todos.razor` file, so, moving forward, the solution will proceed on a new branch:

[Delete feature](https://github.com/TroelsMortensen/BlazorTodoApp/tree/2DeleteFeature)

### First, the goal

The goal is to add an extra column to the table view of the Todos overview. This column will contain a delete button, which will delete the Todo of that row.

The result will look something like this:

![img.png](Resources/RemoveTodoResultView.png)

### Code block

We will start by adding a method in the code block of `Todos.razor`, which can delete a Todo item. It looks like this:

```csharp
private async Task RemoveTodo(int id)
{
    errorLabel = "";
    try
    {
        Todo todoToRemove = todos.First(todo => todo.Id == id);
        await TodoHome.DeleteAsync(id);
        todos.Remove(todoToRemove);
    }
    catch (Exception e)
    {
        errorLabel = e.Message;
    }
}
```

Let us go over the method.\
First, in line 3 we have a `string errorLabel`, that means you must also add this as a field variable.
It is, as always, used to provide a message to the user, if something goes wrong. That is also the reason of the try-catch.

Line 6 uses the `First()` method to find the first `Todo` in the `todos` collection, which matches the given `id`.
In line 7 we make an asynchronous call to the `TodoHome` to delete by id. This call is `await`ed.
Finally, in line 8, we also remove the relevant `Todo` from the `todos` collection, i.e. the collection displayed in the table.\
An alternative approach would be to re-fetch all the todos through the `TodoHome`.

Note: Now we have the `errorLabel` and as mentioned earlier, the `TodoHome.GetAsync()` call in the `OnInitializedAsync()` method could actually fail, if something goes wrong behind the scenes. 
We should surround it with a try-catch, and inside the catch, we update the `errorLabel` with an error message. The slide and code on GitHub will be updated later to reflect this.


### The view

Next up, we need to add that new column with the delete button to the table.\
Update the relevant part of the view like this:

```razor{10,21-27,33-36}
else
{
    <table class="table">
        <thead>
        <tr>
            <th>Owner ID</th>
            <th>Todo ID</th>
            <th>Title</th>
            <th>Completed?</th>
            <th>Remove</th>
        </tr>
        </thead>
        <tbody>
        @foreach (var item in todos)
        {
            <tr>
                <td>@item.OwnerId</td> 
                <td>@item.Id</td> 
                <td>@item.Title</td> 
                <td>@item.IsCompleted</td>
                <td>
                    <label 
                    @onclick="@(() => RemoveTodo(item.Id))" 
                    style="cursor:pointer; color: red; font-weight: bold">
                        &#x2717;
                    </label>
                </td>
            </tr>
        }
        </tbody>
    </table>
}
@if (!string.IsNullOrEmpty(errorLabel))
{
    <label style="color: red">@errorLabel</label>
}
```

Notice the highlighted changes.

In **line 10**, a new column header is added.

**Lines 21-27**: a new cell per row is added. This contains a label with the content `&#x2717;` which will become a fancy _x_. The styling here is inlined in the `<label>` tag.
You are welcome to move this to a style-behind, if you wish. In general that is probably better practice.

In **line 23** we have the `onclick` action, i.e. what happens, when this icon is clicked: We execute a lambda expression. 
We use a lambda expression to call the `RemoveTodo()` method, and parse the argument `item.Id`, which we get from the `item` variable of the surrounding `foreach`-loop. 

With previous buttons, we were able to directly reference a method just be the name, but because we need a variable argument passed along in this case, we need to use a lambda expression instead.

**Lines 33-36** is the `errorLabel` to display any errors, if needed. This is perhaps a crude way, and a popup or something other, could be more user-friendly. This could be an exercise left to the reader.

### Test
Now, you should have a view looking something like the one shown above.

![img.png](Resources/Test.png)

And you can test it by clicking one of the red crosses, and either go to another page and back or refresh. Just to make sure the removal is persisted to the json-file.


![img.png](Resources/AbsolutelyWonderful.png)

