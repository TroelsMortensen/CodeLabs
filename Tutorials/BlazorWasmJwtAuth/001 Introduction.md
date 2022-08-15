# Welcome

This is a first draft. If you have comments, input, feedback, whatever, send me an email: trmo@via.dk.

This tutorial will take you through how to set up a simple login system for Blazor-WASM in combination with a Web API.

This approach will use your own User/Profile/Account type, or whatever you call it. You will manage storage of the users.
We will use the built in `AuthenticationStateProvider` class, extend it to provide the necessary authorization information.

We will not be using the Identity framework. That's a different approach.

We will go through how to set it up from a new default Blazor-WASM project.
Later, you can then reuse the steps to add login/authorization functionality to other Blazor-WASM apps.

## Overview
We are going to use an approach called JWT authentication.

This means that when logging in through the front end, the request is sent to the Web API, and (if the credentials are valid) a JSON Web Token (JWT) is sent back again, containing various information, including information about the User profile.

So, we are going to use a Web API for the back-end and Blazor-WASM for the front end.\
We will use various built-in tools in Blazor to manage authentication and authorization of various sites of the app.\
We will secure our Web API endpoints with authentication and authorization too, so that other programmers don't just create their own app and abuse our endpoints.


### Disclaimer
The purpose of this tutorial is to get a login-system up and running, but not necessarily understand every detail. There will be black box magic moments, where the interested reader can do further research.


### Result
The final project can be found on GitHub [here](https://github.com/TroelsMortensen/JwtAuth)