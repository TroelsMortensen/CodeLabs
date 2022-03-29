# Todos page code
The result can be found on my GitHub project [here](https://github.com/TroelsMortensen/BlazorTodoApp/blob/Part1/Blazor/Pages/Todos.razor).

Open your Todos.razor file.

We will start by updating the code, then the view. Modify the page like so:

```razor
@page "/Todos"
@using Domain.Models
@using Domain.Contracts
@inject ITodoHome TodoHome

<h3>Todos</h3>

@code {
    private ICollection<Todo> todos;

    protected override async Task OnInitializedAsync()
    {
        todos = await TodoHome.GetAsync();
    }
}
```

What's going on?

**Line 1**: The page directive, meaning we can navigate to this page by URI: https://localhost:7140/Todos.

**Line 2-3**: Here we import namespaces from the Domain component, the Models and Contracts.

**Line 4**: This here is important. 
We _inject_ a dependecy, of type `ITodoHome`. Whenever this page is opened, 
the framework will create an instance of `ITodoHome` (currently that is `TodoFileDAO`). 
This means our page just knows about the interface, and we can later, easily, swap out which implementation is used. 
This is pretty cool. This is the standard approach, you should probably never create a new service manually in your pages, 
always ask for them with the `@inject`.

**Line 9**: This field holds our todos, which will be shown on the page.

**Line 11**: This method is automatically called, whenever the page is opened. That means, we can use this method to load relevant data, or set things up.

**Line 13**: Here we fetch the todos from the ITodoHome instance (the TodoFileDAO, which uses FileContext). We *await* the method call, because this may potentially take some time, and we want to release control, so the process can do other stuff. In general, we use async wherever we can in our pages.

That's all for the code, for now. Next the view.

Note: The `TodoHome.GetAsync()` call could actually fail, if something goes wrong behind the scenes. We should surround it with a try-catch, and inside the catch, we update an `errorLabel`. The slide and code on GitHub will be updated later to reflect this.
The `errorLabel` is currently not introduced until the delete feature is implemented.