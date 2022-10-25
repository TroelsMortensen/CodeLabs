# Get Users

Next, we implement the functionality of retrieving a collection of Users. The method is already there in UserEfcDao. It just needs a body.

We don't convert the Users to IEnumerable, like we did in UserFileDao. This time we use IQueryable.

Otherwise, the method is very similar:

```csharp
public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
{
    IQueryable<User> usersQuery = context.Users.AsQueryable();
    if (searchParameters.UsernameContains != null)
    {
        usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(searchParameters.UsernameContains.ToLower()));
    }

    IEnumerable<User> result = await usersQuery.ToListAsync();
    return result;
}
```

The `IQueryable` is a _representation_ of a query, not yet loaded, but in the process of being constructed.\
In the if-statement we expand on the query, i.e. the SQL expression, we are building.

Only when we use the result, i.e. by converting to list, do we actually execute the query against the database.\
This is an important point. If you initially convert to list or similar, you'll load the entire table. This is not efficient.

## Test Setup
Before testing, let's add a few more users. You can do this through the Swagger UI.\
Or, open the Database tool again, double click on the Users table. You can add more rows here.

![](Resources/AddUsers.gif)

Notice how the Id is generated automatically.

Now run the Web API, and test the GET /Users endpoint. Remember you can include filtering, by the user name containing some string.\
Test that it works with both upper case and lower case letters.