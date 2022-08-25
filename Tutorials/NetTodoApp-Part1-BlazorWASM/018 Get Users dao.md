# Get Users from Data Access Layer

Now we need to fix the UserFileDao.cs. The compiler should be complaining, because we have added a method to IUserDao.

Implement the method in UserFileDao. It looks like this:

```csharp
public Task<ICollection<User>> Get(SearchUserParametersDto searchParameters)
{
    IEnumerable<User> users = context.Users.AsEnumerable();
    if (searchParameters.UsernameContains != null)
    {
        users = context.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
    }

    ICollection<User> result = new List<User>(users);
    return Task.FromResult(result);
}
```


....