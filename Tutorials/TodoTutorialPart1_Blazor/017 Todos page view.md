# Todos page view
We wish to show the Todo items in a table, at least initially.

This means we must update the view part of the page, i.e. just below `<h3>Todos>/h3>`, and above the `@code {`.

Insert the following:

```razor
@if (todos == null)
{
    <p>
        <em>Loading...</em>
    </p>
}
else if (!todos.Any())
{
    <p>
        <em>No ToDo items exist. Please add some.</em>
    </p>
}
else
{
    <table class="table">
        <thead>
        <tr>
            <th>Owner ID</th> <th>Todo ID</th> <th>Title</th> <th>Completed?</th>
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
            </tr>
        }
        </tbody>
    </table>
}
```

What's up?

**Line 1**: Initially when the page is shown, the todos field variable will not have been initialized, it's `null`. So, if that's the case, we will render the html in lines 3-5.

**Line 7**: Maybe you load the list of todos, but there are none, the list is empty. Then we render lines 9-11 to the user.

**Line 13**: If there are any Todos, we render a table, defined in lines 15-32.

**Lines 16-20**: This is just basic html for a table header, nothing fancy here.

**Line 22**: Here we have a for-loop, embedded in the page. This is razor syntax. It is marked with an '@', to indicate the start of the C# code.
You will notice, html and C# can be mixed. Whenever the user requests this page, the framework will execute any code to generate the final html result.
In this case, we loop over each Todo, and insert a table row for each item.

You can embed razor syntax just about anywhere, just mark it with `@( your code here )`. Usually to generate html like the loop above, but you can also add/remove specific css classes to tags, or styles, if needed.
Or, in the future, maybe the todos should be generated as cards, and the html for that is in a separate component.

Again, the result of this file can be found on my GitHub project [here](https://github.com/TroelsMortensen/BlazorTodoApp/blob/Part1/Blazor/Pages/Todos.razor).
