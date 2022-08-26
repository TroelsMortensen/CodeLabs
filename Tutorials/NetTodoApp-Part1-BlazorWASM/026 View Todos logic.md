# View Todos Logic Layer

We wish to retrieve a list of Todos, and we want to be able to apply filtering. This is very similar to what we did with the User search.
* We need a DTO to contain the search criteria
* We need a method in the Logic interface to take the search criteria and return a collection of Todos
* We need a method in the DAO interface with a similar method
* The logic implementation does nothing other than forward the method call

## Search Criteria DTO
We need to be able to search by the following:
* User name
* User ID
* Completed status
* Title contains a piece of text

So, we wrap these criteria into a DTO class, put it in Domain/DTOs, call it "SearchTodoParametersDto":

```csharp
public class SearchTodoParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public bool? CompletedStatus { get;}
    public string? TitleContains { get;}

    public SearchTodoParametersDto(string? username, int? userId, bool? completedStatus, string? titleContains)
    {
        Username = username;
        UserId = userId;
        CompletedStatus = completedStatus;
        TitleContains = titleContains;
    }
}
```

All properties are nullable, indicated with the `?`, meaning the property can be null. That is not a surprise for the strings, you should by now know strings can be null.\
But simple types like ints and booleans, they cannot normally be null. We do however need a way to say that e.g. `CompletedStatus` should not be applied when filtering. This then gives us three applications for `CompletedStatus`:

1) Include only completed Todos, value is `true`
2) Include only un-completed Todos, value is `false`
3) Don't apply this filter, value is `null`

The same reasoning holds for UserId.

## Logic Interface
We need the following method in the ITodoLogic interface:

```csharp
Task<IEnumerable<Todo>> Get(SearchTodoParametersDto searchParameters);
```

## Data Interface

We need the same method signature in the ITodoDao interface.

## Logic Implementation
Implement the method from the interface, all it does is call the same method on the `todoDao` field variable. Same as what we did when getting the collection of users.
