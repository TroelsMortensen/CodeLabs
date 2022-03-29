# The End

If you have implement support for users, you'll need another class to manage that, similar to the `TodoHttpClient`. 

This class should (among other things) be able to GET and POST users.\
From the Login tutorial, you might have an IUserService interface. This is the interface, which implementation must be replaced by a new class. The AuthService still belongs to the Blazor project, however.

The next step, in another tutorial, will be to add a database to the application, using Entity Framework Core.
