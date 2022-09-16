# Get Todo By Id

If we were to have a feature like

> As a User I can update title of a Todo, so that I can fix mistakes made when created

or

> As a User I can re-assign a Todo, so that others can take over if the assignee is too busy
 
The flow would be to first retrieve the relevant Todo from the server, make various modifications on the client side, and send those changes back again to the server.

We will include this feature in the client, in the next tutorial.


## Implement the functionality
We can already update Todos with the PATCH endpoint implemented previously. But we cannot get a single Todo. We could use the endpoint, which returns a collection of Todos, but for practice, let's make a specialized endpoint. 

You are to implement this functionality now. Here are a few key points:

1) Web API
   1) The endpoint is a GET with URI /todos/{id}
2) Logic interface
   1) The argument is an int
   2) The result is the Todo, or a DTO, that is up to you. Maybe you don't want to send back the nested User object, but just the user name
   3) Remember asynchronous
3) Logic implementation
   1) Nothing here really, other than forwarding the call to the Data Access Layer
   2) Alternatively, if you decide to return a DTO, the DAO already has a method to retrieve a Todo by id. You would then convert this to a DTO.
5) DAO 
   1) There is already a `GetByIdAsync()` method. You could reuse this.


[My version is found here](https://github.com/TroelsMortensen/WasmTodo/tree/008_GetTodoById)



