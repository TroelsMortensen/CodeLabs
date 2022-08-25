# Get Users from Data Access Layer

Now we need to fix the UserFileDao.cs. The compiler should be complaining, because we have added a method to IUserDao.

Implement the method in UserFileDao. It looks like this:

```csharp
public Task<IEnumerable<User>> Get(SearchUserParametersDto searchParameters)
{
    IEnumerable<User> users = context.Users.AsEnumerable();
    if (searchParameters.UsernameContains != null)
    {
        users = context.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
    }

    return Task.FromResult(users);
}
```

First, the method signature. The return type is Task, as usual. We get a collection of users, matching the search criteria.

The first line of code takes the users from the context, and converts that ICollection to an IEnumerable. That's because of how the filtering is going to work, it uses IEnumerables. It's also a type of collection, just with fewer methods, it cannot be modified.

We then check if the search parameter is not null, in which case we want to apply it.\
We do that in line 6 with the `Where()` method, which goes through all the users, 
and selects those that matches the criteria specified by the lambda expression.

In the end the result is returned.

If we had more search parameters, we would for each of them make an if-statement to check if they should be applied.\
With this approach we initially take all the users, and whittle them down search parameter by search parameter.


