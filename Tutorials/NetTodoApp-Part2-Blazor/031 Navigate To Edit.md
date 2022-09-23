# Navigate to the Edit Todo Page

The testing was done by manually typing in the URI in the address bar of the browser. That is inconvenient for a user.

We will add a column to the table View Todos, with a button, which when clicked, will take you to the EditTodo page.

![img.png](Resources/LetsDoThis.png)

## Icon

We need a new icon to press. We will use this one:

![](Resources/edit.gif)

Download it like the funnel icons. Place it in the same folder: wwwroot/icons.

## The View First

Open ViewTodos.razor. We will modify the table, here's the snippet part:

```razor{1,16,29-31}
@inject NavigationManager navMgr


...


else
{
    <table class="table">
        <thead>
        <tr>
            <th>Todo ID</th>
            <th>Owner ID</th>
            <th>Title</th>
            <th>Completed?</th>
            <th>Edit</th>
        </tr>
        </thead>
        <tbody>
        @foreach (var item in todos)
        {
            <tr>
                <td>@item.Id</td>
                <td>@item.Owner.UserName</td>
                <td>@item.Title</td>
                <td>
                    <FancyCheckBox IsCompleted="@item.IsCompleted" OnChange="@((status) => CompleteTodo(item, status))"/>
                </td>
                <td>
                    <img src="icons/edit.gif" class="funnel" @onclick="@(() => navMgr.NavigateTo($"/EditTodo/{item.Id}"))"/>
                </td>
            </tr>
        }
        </tbody>
    </table>
}

...
```

Okay, we need a NavigationManager, so that is injected at the top of the page, along with the other inject statements.

Then we have the part of the view with the table definition. Notice the highlighted:
* There is a new column, defined by the new table header with "Edit"
* And then the content of that column is defined in the new `<td>`. We insert an image, the source being our gif above. The style class is the same as the funnel-filter icons. That is just lazy, but it works for now. When the icon is clicked, we use the NavigationManager to navigate to the page EditTodo, and we add the ID of the Todo displayed in this row.

## Testing, Testing, 1, 2, Testing

Let's do this.

Run the usual, navigate to View Todos. At the right there is a new column with the edit gifs. Click on one, and pay attention to the data of that row, it should now be displayed on the EditTodo page.

