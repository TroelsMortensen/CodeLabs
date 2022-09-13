# Completing a Todo Item
New feature. We are working towards CRUD operations for the Todo, i.e. create, read, update, delete.

And we currently have create, and read. 
Now it is time to update Todos, part of them at least, 
because we want to be able to mark a Todo as completed.

> As a User I can complete a Todo, so that I can mark things as done

We want to be able to change the completed status. 

Later, we also want to be able to change the owner, so that a Todo can be reassigned. And maybe it is at some point needed to change the title of the Todo.\
This means we want to be able to send a Todo item to the server, have it verify the data, and then update an existing Todo. 
We are therefore doing a little more than what is needed for this user story. 
But, it is little extra work, and otherwise we might later have to go back and revise existing code, so that another property can be updated, and then again for a third property. 
So, we batch this together.

### RESTful
It would be preferable to implement the three features above separately:
1) complete a Todo
2) re-assign a Todo
3) update the title of a Todo

This would then result in three different endpoints in the Web API, i.e. three PATCH endpoints, because we use PATCH when updating a resource.\
However, a RESTful Web API is organized around _resources and CRUD operations_, not all kinds of specific _actions_.

We could abuse the Web API and make three endpoints with URIs like:

PATCH  /todos/complete/{todo-id}\
PATCH  /todos/reassign/{todo-id}\
PATCH  /todos/update/{todo-id}

But this goes against the REST standard.

Instead, we have to bundle all kinds of updates to the Todo into one endpoint:

PATCH /todos

And have just one vertical path through the server for everything update related.\
Regarding the URI, we could put an ID here, or the ID can go into the argument. We will just do the latter.

So, if you are in this position, you will have to make a choice. In our case, we follow conventions and stick with the one endpoint.


## Let's go
Same approach: application first, then data layer, and then Web API.

[This feature is found here](https://github.com/TroelsMortensen/WasmTodo/tree/006_CompleteTodo)