# Add Todo Controller Endpoint
We have the logic and data layer in place. We just need to provide access to the client, and for that we need:

* A new Controller class to handle Todos
* A POST endpoint to handle the creation of Todos


## The Controller Class
Create a new "TodosController" in WebAPI/Controllers.

Set it up similarly to the UsersController, with the attributes and the inheritance. We need an ITodoLogic field variable as well.

<details>
<summary>hint</summary>

```csharp
[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoLogic todoLogic;

    public TodosController(ITodoLogic todoLogic)
    {
        this.todoLogic = todoLogic;
    }
}
```
This is almost identical to the other Controller class, we made, so if you forgot how things work, go back to slide 12.

</details>

## The Endpoint

We now need the endpoint. You'll give this a go yourself as well first, it is very similar to the endpoint for creating a user.

* Mark it as a POST endpoint
* Hand over the data to the logic layer
* Return an informative response to the client
* In case of any errors, it is, as always, important to return information to the client

<details>
<summary>hint</summary>

```csharp
[HttpPost]
public async Task<ActionResult<Todo>> Create(TodoCreationDto dto)
{
    try
    {
        Todo created = await todoLogic.Create(dto);
        return Created($"/todos/{created.Id}", created);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e.Message);
    }
}
```
This is almost identical to the other Controller class, we made, so if you forgot how things work, go back to slide 12.

</details>

See the final class [here](https://github.com/TroelsMortensen/WasmTodo/blob/004_AddTodo/WebAPI/Controllers/TodosController.cs)