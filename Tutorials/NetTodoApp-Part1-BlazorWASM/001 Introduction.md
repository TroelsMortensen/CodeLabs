Der skal væres users, og en page, hvor man kan tilføje en ny user.

Der skal være clean architecture på server side.
Huske fejl beskeder, også i OnINitialized.

Users skal kunne vælges i en drop down.

Ved input skal jeg ikke bruge det indbyggede EditForm. Brug basic html.

catch clauses skal stadig printe til console, nævn at print til file er pænere

skriv mange gange, det er vigtigt at give feedback til brugeren.

Få en HTtpClient gennem constructor. De skal åbenbart ikke disposes.

Seed fra Program.cs file. Lav mange todos, e.g. 25, og så brug noget paging og indexing.



Beskriv domain, ansvar, H or for acces interfaces, hvorfor dao interfaces. Hvorfor ligeglad med omkringliggende layers, de er ligegyldige detaljer. 

why is it called contracts component? What is a contract?

component diagram over inter-component dependencies.

A common case is that the data is structured on way on the server, but it is displayed in a different way in the client. For that we can use DTOs.


----


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



#### A final comment
I have been renaming things over and over in this tutorial. I hope I have made the necessary updates to the text here, whenever things were updated in the code. 

> There are only two hard things in Computer Science: cache invalidation, naming things, and off-by-one errors.

-- Phil Karlton