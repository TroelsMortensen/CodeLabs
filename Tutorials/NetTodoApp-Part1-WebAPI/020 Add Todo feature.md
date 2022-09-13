# Add Todo

We have the necessary user functionality in place. Now it's time to get started in the features related to the Todo items.

We will start with:

> As a User I can add a new Todo, so that I can remember important things

We want to create a new Todo item, it contains some data, and is assigned to a specific user.

Same approach as usual:
1) Application layer
   1) Logic interface
   2) DAO interface
   3) Logic imple
2) Data Access layer
   1) JSON DAO implementation
3) Web API layer
   1) New controller with endpoint

The order of 2 and 3 is not really relevant, they could be switched. If you want to do some integration testing along the way, the order may be relevant for you.

[This feature is found here](https://github.com/TroelsMortensen/WasmTodo/tree/004_AddTodo).