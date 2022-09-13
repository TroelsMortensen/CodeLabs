# Delete Todo Logic Layer

This should also be familiar.

First we consider input, output, and behaviour.

Then we define interfaces for logic and dao. And then we implement the logic.

#### Input/output
The input is just an ID, we need no more information than that. That is true for both logic and Data Access interfaces.

The output is nothing.

#### Behaviour

Is there any logic associated with deleting a Todo?

Suggestions:
* A Todo can only be deleted, if it is completed
* A Todo can only be deleted by certain Roles, if we had those
* A Todo can only be deleted _x_ hours/days after it was completed

You can probably come up with more ideas yourself. We will do only the first point.

## Logic/DAO Interface

The input is an ID, and there is not output. The two interfaces need the same method signature:

```csharp
Task DeleteAsync(int id);
```

Put this method in both interfaces

## Logic Implementation

We will do the check of the completed status, so only completed Todos can be deleted.

The method in the TodoLogic class must then:

* Fetch existing Todo
* Check that the completed status is `true`, otherwise throw exception
* Call the `DeleteAsync()` method on ITodoDao interface

Give it a go, then look at the hint.

<details>
<summary>hint</summary>

```csharp
public async Task DeleteAsync(int id)
{
    Todo? todo = await todoDao.GetByIdAsync(id);
    if (todo == null)
    {
        throw new Exception($"Todo with ID {id} was not found!");
    }

    if (!todo.IsCompleted)
    {
        throw new Exception("Cannot delete un-completed Todo!");
    }

    await todoDao.DeleteAsync(id);
}
```

The Todo is found, and checked for existence. The completed status is checked. If all is good, the id is passed to the Data Access layer for handling. 

</details>