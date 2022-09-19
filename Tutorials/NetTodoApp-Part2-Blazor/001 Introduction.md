Huske fejl beskeder, også i OnINitialized.

Users skal kunne vælges i en drop down.

Ved input skal jeg ikke bruge det indbyggede EditForm. Brug basic html.

Få en HTtpClient gennem constructor. De skal åbenbart ikke disposes.


# Introduction
This is the _second_ part of a 3 part tutorial series.

You should already have the Todo Web API in place from the first part. We will continue working in the same Solution so as to reuse certain things.

This tutorial will cover the client side, i.e. making a Blazor WASM app.

## Features

We already have the server-side of multiple features from part 1. We will now complete the client side of those features.

1) As a user of the system I can add a new User, so that Todos can be assigned to Users.
2) As a User I can get a list of all existing Users, so that I can assign Todos to them
3) As a User I can add a new Todo, so that I can remember important things
4) As a User I can view all or filtered Todos, so that I can remember what to do
5) As a User I can complete a Todo, so that I can mark things as done
6) As a User I can delete a Todo, so that I can clean up


## Branches

Again, each feature has its own branch on GitHub. Below is an overview:

* [009_ClientSetup]() Here the initial setup of the Client components are done.