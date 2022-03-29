# Updating the overview to edit Todo

Once again, we need a new column to keep a button for editing a Todo.

We need an icon for edit. I found a gif from [this icon site](https://icons8.com/icons/set/edit)

You can find the gif I use [here](https://github.com/TroelsMortensen/BlazorTodoApp/blob/5EditTodo/Blazor/wwwroot/img/edit.gif), just right-click and save as on the gif.

```razor{8,25-27}
    <table class="table">
        <thead>
        <tr>
            <th>Owner ID</th>
            <th>Todo ID</th>
            <th>Title</th>
            <th>Completed?</th>
            <th>Edit</th>
            <th>Remove</th>
        </tr>
        </thead>
        <tbody>
        @foreach (var item in todosToShow)
        {
            <tr>
                <td>@item.OwnerId</td>
                <td>@item.Id</td>
                <td>@item.Title</td>
                <td>
                    <label class="switch">
                        <input type="checkbox" checked=@item.IsCompleted @onchange="@((arg) => ToggleStatus((bool)arg.Value, item))">
                        <span class="slider round"></span>
                    </label>
                </td>
                <td>
                    <img src="img/edit.gif" class="funnel" @onclick="@(() => Edit(item.Id))"/>
                </td>
                <td>
                    <label @onclick="@(() => RemoveTodo(item.Id))" style="cursor:pointer; color: red; font-weight: bold">
                        &#x2717;
                    </label>
                </td>
            </tr>
        }
        </tbody>
    </table>
```

Notice the new `<th>` in line 8.

The corresponding column is defined in lines 25-27. I reference the gif, the class is `funnel`, a bit of a hack. Alternatively you can create a new style class for this gif icon, with the following settings:

```css
width:30px; 
height:30px;
cursor: pointer;
```

This should display the icon nicely.

You must inject a `NavigationManager` at the top: `@inject NavigationManager navMgr`

Last part is the `Edit` method:

```csharp
private void Edit(int itemId)
{
    navMgr.NavigateTo($"EditTodo/{itemId}");
}
```

### Test

You should now be able to 
1) open the Todos page
2) click on the edit icon for one of the rows
3) this takes you to the edit page, with the current Todo data shown
4) Modify the data
5) Click <kbd>Update</kbd> which takes you back to Todos overview
6) Verify the changes.