# Introduction

This is the _third_ part of a 3 part tutorial series.

You should already have the Todo Web API in place from the first part, and the Blazor-WASM app from the second tutorial. 
We will continue working in the same Solution.

This tutorial will cover swapping out the current JSON-data layer with a layer using Entity Framework Core and SQLite.

We will make modifications to the server side. The client will not be touched, as it does not care how the data is saved/loaded.

## Didn't do Parts 1 and 2?

If you don't have a working app based on part 1 and 2, you can just clone a branch from GitHub, which will give the code from 1 and 2.

This is the branch:  https://github.com/TroelsMortensen/WasmTodo/tree/018_PopupSuccessMessage

It is the last of part 2. If you clone this, you will have the code, which is the starting point of this tutorial.

## Features

We still have the same features, i.e. user stories as in the previous two tutorials, however, they are less interesting in this part.

Instead, we must make substitute classes for `TodoFileDao` and `UserFileDao`. The new classes will implement the same interfaces,
which will make it easy ot let the Web API use the new layer instead of the old. It requires only a few modifications to WebAPI/Program.cs, where we register new implementations of the interfaces.\
The new EFC implementations will then be injected into the Logic layer.

We will still implement the methods in the same order as the user stories, so that we can test along the way.

## Branches
There will be only two branches:

1) [One for setting up the initial EFC stuff](https://github.com/TroelsMortensen/WasmTodo/tree/019_EfcSetup)
2) [One for doing the most of the actual implementation](https://github.com/TroelsMortensen/WasmTodo/tree/020_EfcAddUser)

## Comment
Notice how some steps are prefixed with a "+". This is because these are the main steps of setting up a database.

This tutorial will contain much information besides the basic setup, e.g. related to Todos and Users and general design discussion, etc.

So, when you need to set up EFC for your project or other, the +-marked steps are the relevant ones.

## Let's go

That should be all, let us get started.
