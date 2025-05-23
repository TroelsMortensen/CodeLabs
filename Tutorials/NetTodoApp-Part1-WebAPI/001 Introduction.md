# Introduction
This is the first part of a 3 part tutorial series (at least according to the current plan). 

The system will be a basic Todo-app. You will be able to create Todo items, update and delete them. 
Get an overview of all Items, and a few other basic features.
This kind of app is essentially a "Hello World".

Together, all three tutorials will go over how to:

1) create a Web API, initially using a file to store data in JSON format
2) create a Blazor WASM front end, which will interact with the Web API
3) swap out the file storage with data base storage, using Entity Framework Core

This tutorial will cover the first point: a Web API.
It will result in a client/server system with a database. 


### GitHub

All code will be [available on GitHub](https://github.com/TroelsMortensen/WasmTodo). Different parts of the tutorial may be available on different branches.

Here's an overview of the branches:

[001_BasicSetup](https://github.com/TroelsMortensen/WasmTodo/tree/001_BasicSetup): will contain the setup of the components, model classes, and the FileContext which stores data.\
[002_AddUser](https://github.com/TroelsMortensen/WasmTodo/tree/002_AddUser): This will contain the code for the feature of adding a new User.\
[003_GetUsers](https://github.com/TroelsMortensen/WasmTodo/tree/003_GetUsers): This contains code for the feature of retrieving Users.\
[004_AddTodo](https://github.com/TroelsMortensen/WasmTodo/tree/004_AddTodo): This branch is for adding a new Todo.\
[005_GetTodos](https://github.com/TroelsMortensen/WasmTodo/tree/005_GetTodos): Here we retrieve Todos.\
[006_CompleteTodo](https://github.com/TroelsMortensen/WasmTodo/tree/006_CompleteTodo): This covers updating a Todo.\
[007_DeleteTodo](https://github.com/TroelsMortensen/WasmTodo/tree/007_DeleteTodo): This feature allows us to delete a Todo.


## Code mismatch
There is a convention that asynchronous methods (which returns Task) have method names suffixed with "Async", e.g. `CreateAsync()`, or `GetUserByIdAsyn()`.

I did not remember to do this until half way through. So, I'm going over the previous parts and renaming everything. I may have forgotten some places, so the code examples in this tutorial, may have method names not matching entirely the names on GitHub. You're welcome to let me know, if you find a mismatch.


#### A final comment
I have been renaming things over and over in this tutorial. I hope I have made the necessary updates to the text here, whenever things were updated in the code. 

> There are only two hard things in Computer Science: cache invalidation, naming things, and off-by-one errors.

-- Phil Karlton

## Functional requirements
As mentioned, we are doing a Todo app. Below are the user stories, which we will implement

1) As a user of the system I can add a new User, so that Todos can be assigned to Users.
2) As a User I can get a list of all existing Users, so that I can assign Todos to them
3) As a User I can add a new Todo, so that I can remember important things
4) As a User I can view all or filtered Todos, so that I can remember what to do
5) As a User I can complete a Todo, so that I can mark things as done
6) As a User I can delete a Todo, so that I can clean up