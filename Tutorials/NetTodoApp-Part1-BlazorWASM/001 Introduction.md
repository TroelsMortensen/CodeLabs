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
There are only two hard things in Computer Science: cache invalidation, naming things, and off-by-one errors.

-- Phil Karlton

# Introduction
This is the first part of a 3 part tutorial series. 

The system will be a basic Todo-app. You will be able to create Todo items, update and delete them. Get an overview of all Items, and other basic features.
This kind of app is essentially a "Hello World".

Together, all three tutorials will go over how to:

1) create a Web API, initially using a file to store data
2) create a Blazor WASM front end, which will interact with the Web API
3) swap out the file storage with data base storage, using Entity Framework Core

This tutorial will cover the first point: a Web API.
It will result in a client/server system with a database. 

On the server side, the Web API, we will use a classic 3-layered architecture:

1) Network layer to receive requests from clients
2) Domain layer, responsible for all business logic
3) Data access layer, responsible for storing/retrieve data from storage

We will let us inspire by existing architecture approaches: Clean, Onion, Hexagon. 
These are basically similar, and all advocate the 3-layered approach.

All code will be [available on GitHub](https://github.com/TroelsMortensen/WasmTodo). Different parts of the tutorial may be available on different branches.