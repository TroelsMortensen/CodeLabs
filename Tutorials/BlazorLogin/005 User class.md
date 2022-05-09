# The user class
This class holds information about the user.

Create a new directory, call it Models (or whatever you please).

Inside this directory, create a new class, User, or whatever you wish. 
In this example it looks like below. Yours could be different.

```csharp
namespace BlazorLoginApp.Models;

public class User
{
    public string Name { get;  set; }

    public string Password { get;  set; }

    public string Role { get;  set; }

    public int SecurityLevel { get;  set; }
    public int BirthYear { get;  set; }

    public User(string name, string password, string role, int securityLevel, short birthYear)
    {
        Name = name;
        Password = password;
        Role = role;
        SecurityLevel = securityLevel;
        BirthYear = birthYear;
    }
}
```

You may define your User with other fields, but these are the ones we will use in this example. 
With this, we can demonstrate privilege by level and privilege by role. Two different approaches. 

#### Privilege by level

![img_4.png](img_4.png)

With privilege by level, each security level is allowed more access than the one before it. 
This could often be seen as "guest", "registered user", "admin".

#### Privilege by role

With the roles, we can provide access to different places of the system, to different types of users.

![img_5.png](img_5.png)

There is not necessarily a hierarchy here, where one can do more than the other. Instead each type is allowed access to different parts of the system.

I have here tried to imply that the Product manager may access *some* customer data, maybe view certain things, but not modify. I.e. only *read access*.

Maybe the Customer Service Assistant view some product orders by customers, but cannot modify anything.