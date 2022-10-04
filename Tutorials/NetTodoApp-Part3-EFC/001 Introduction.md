# Introduction

This is the _third_ part of a 3 part tutorial series.

You should already have the Todo Web API in place from the first part, and the Blazor-WASM app from the second tutorial. 
We will continue working in the same Solution.

This tutorial will cover swapping out the current JSON-data layer with a layer using Entity Framework Core and SQLite.

We will make modifications to the server side. The client will not be touched, as it does not care how the data is saved/loaded.

## Features

We still have the same features, i.e. user stories as in the previous two tutorials, however, they are less interesting in this part.

Instead, we must make substitute classes for `TodoFileDao` and `UserFileDao`. The new classes will implement the same interfaces,
which will make it easy ot let the Web API use the new layer instead of the old. It requires only a few modifications to WebAPI/Program.cs, where we register new implementations of the interfaces.\
The new EFC implementations will then be injected into the Logic layer.

We will still implement the methods in the same order as the user stories, so that we can test along the way.

## Branches
The data layer methods implemented for each user story is planned to be in its own branch, similar to what you have seen so far.\
I might merge some features, if there is no change to existing code.

## Let's go

That should be all, let us get started.
