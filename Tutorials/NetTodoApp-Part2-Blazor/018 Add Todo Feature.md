# Add New Todo

We are ready for the next feature:

> As a User I can add a new Todo, so that I can remember important things

The user should input title, and assignee of a Todo. The assignee is to be selected from a drop-down menu. 
This may not scale too well, if there are hundreds of users, but we will keep it simple for now. Later we might introduce some search functionality to find a specific User.

We need:
* Create a new Service interface, responsible for Todos.
* Implement a new HTTP service, responsible for Todos.
* Create a page, where the user can input the relevant data.

[This feature is found in this branch](https://github.com/TroelsMortensen/WasmTodo/tree/012_CreateTodo)