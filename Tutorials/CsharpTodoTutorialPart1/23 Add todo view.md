# The view part of adding a new Todo item
In the view part of the page, insert the below:

```razor
<EditForm Model="@newTodoItem" OnValidSubmit="@AddNewTodo">
    <DataAnnotationsValidator/> 
    <ValidationSummary/>
    <div class="form-group">
        <span>
            <label>User Id:</label>
        </span>
        <span>
            <InputNumber @bind-Value="newTodoItem.OwnerId"/>
        </span>
    </div>
    <div class="form-group">
        <span>
            <label>Title:</label>
        </span>
        <span>
            <InputTextArea rows="4" @bind-Value="newTodoItem.Title"/>
        </span>
    </div>
    <p class="actions">
        <button class="btn btn-outline-dark" type="submit">Create</button>
    </p>
</EditForm>
<label>@errorLabel</label>
```
Note that the author is no expert in css and html, and you are welcome to rearrange things to beautify the page, as needed.

**Line 1**: This is an EditForm component, from Blazor. We pass arguments to the component, the `Model` is the `newTodoItem` from the code-block.
`OnValidSubmit` points to which method to call, when the Create button is pressed, and the inputted data is valid according
to the Todo data validation attributes. There is also a `OnInvalidSubmit`, in case you want to do something specific, when the inserted data is invalid.

**Line 2**: Another component from Blazor, which collaborates with `EditForm`. This is the component which actually validates the Todo.

**Line 3**: This will display a summary in case of invalid data.

**Line 9**: This InputNumber component works with EditForm. Here we data-bind. 
This is done with `@bind-Value=newTodoItem.Id`. 
Notice the capital 'V' in Value. Much important!
This data-binding means, that whatever we type into the input field will be assigned to the OwnerId of the Todo. 
Data-binding is widely used.

**Line 17**: This is a text area blazor component, again working together with EditForm. 
Here we also data-bind so the value of the text area is assigned to the Title of the Todo item.

**Line 21**: Here we have the submit button, notice the *type* attribute. When this is clicked, the `<DataAnnotationsValidator/>` will validate the data,
and if there are no problems, the `AddNewTodo` method will be called.

**Line 24**: This is the errorLabel, which will show errors from trying to save the Todo.

There are a handful of InputSomething components, e.g. InputText. These can be googled as need.

This concludes the AddTodo page. The final step is to add another navigation menu item, similar to what we did with the Todos page. I leave that to the reader.

The final class can be found [here](https://github.com/TroelsMortensen/BlazorTodoApp/blob/Part1/Blazor/Pages/AddTodo.razor)