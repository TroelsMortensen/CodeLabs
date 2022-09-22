# View All Todos Page

First, create a new Blazor page in the Pages directory. Call it "ViewTodos".

This page is going to be a bit more complicated, we will expand it over a few iterations.

Initially it will just display all Todos.\
Then we add filtering functionality.\
Then the next user story is about completing a Todo, so we expand this page for that.\
We will also be able to delete Todos.\
We can edit Todos.\
And finally we will show a popup.

All these things, one thing at a time.


First, just load and display all Todos.

## The Code
We need a method to fetch the Todos. So the code block looks like this:

```csharp
@code {
    private IEnumerable<Todo>? todos;
    private string msg = "";

    private async Task LoadTodos()
    {
        try
        {
            todos = await todoService.GetAsync(null, null, null, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            msg = e.Message;
        }
    }
}
```

We have a field to hold the Todos, and a `msg` for any messages to the user.

The method will get the Todos from the `ITodoService`. The method takes the four filter criteria, but we have none, currently

## The View
The view code is shown below:

```csharp
@page "/ViewTodos"
@using Domain.Models
@using HttpClients.ClientInterfaces
@inject ITodoService todoService

<h3>Todos</h3>

<div>
    <button @onclick="LoadTodos">Load</button>
</div>

@if (todos == null)
{
}
else if (!todos.Any())
{
    <p>No Todos to display</p>
}
else
{
    <table class="table">
        <thead>
        <tr>
            <th>Todo ID</th>
            <th>Owner ID</th>
            <th>Title</th>
            <th>Completed?</th>
        </tr>
        </thead>
        <tbody>
        @foreach (var item in todos)
        {
            <tr>
                <td>@item.Id</td>
                <td>@item.Owner.UserName</td>
                <td>@item.Title</td>
                <td>@item.IsCompleted</td>
            </tr>
        }
        </tbody>
    </table>
}
@if (!string.IsNullOrEmpty(msg))
{
    <label style="color: red">@msg</label>
}
```

The usual at the top, page directive, imports, injects.

We have the button in line 9 to call the `LoadTodos()` method.

Then the usual checks we do, when we want to display a collection: if-else if-else.

The else-part is the only interesting thing here. We define a table. Lines 24-27 are the column titles.\
The rows are generated with a foreach-loop, iterating over the Todos.\
Each row consists of some table data, each cell is in the `<td>` tags. Here we pull the data from the Todos to display in the view.\
Notice the `Owner` is a User object, therefore, we need to go a step deeper for the actual user name.

#### Styling
We will skip styling, the reader is welcome to apply some themself. Define the css in either a style-behind (we won't do other tables, so isolating the css might be fine), or use one of the global css files.

## Test

This should be working now. Run your Web API and Blazor app, navigate to the View Users page, and press the load button. You should see your Todos displayed in the table.

We will now expand this page with filtering functionality.