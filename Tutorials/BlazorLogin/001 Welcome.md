# Welcome to Blazor-server login
This tutorial will take you through how to set up a simple login system for Blazor-server. It can possibly be converted to Blazor-wasm, but I haven't attempted that.

This approach will use your own User/Profile/Account type, or whatever you call it. You will manage storage of the users.
We will use the built in `AuthenticationStateProvider` class, extend it to provide the necessary authorization information.

We will not be using the Identity framework. That's a different approach.

This is mostly a toy example to be used for your semester projects.

We will go through how to set it up from a default Blazor project. Later, you can then reuse the steps to add login/authorization functionality to other Blazor-server apps.