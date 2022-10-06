# Welcome

This tutorial will take you through how to set up a simple login system for Blazor-WASM in combination with a Web API.

This approach will use your own User/Profile/Account type, or whatever you call it. 

We will not be using the Identity framework. That's a different approach.

We will go through how to set it up from a new default Blazor-WASM project and default Web API.
Later, you can then reuse the steps to add login/authorization functionality to other (existing) Blazor-WASM apps or Web APIs.
The intention is, that this approach can be applied to an existing system without too much trouble.

## Overview
We are going to use an approach called JWT authentication.

This means that when logging in through the front end, the request is sent to the Web API, and (if the credentials are valid) a JSON Web Token (JWT) is sent back again, containing various information, including information about the User profile.

We are going to use a Web API for the back-end and Blazor-WASM for the front end.\
We will use various built-in tools in Blazor to manage authentication and authorization of various sites of the app.\
We will secure our Web API endpoints with authentication and authorization too, so that other programmers don't just create their own app and abuse our endpoints.


### Disclaimer
The purpose of this tutorial is to get a login-system up and running, but not necessarily understand every detail. There will be black box magic moments, where the interested reader can do further research.

The actual security of this approach is somewhat uncertain. There are more considerations to take, if this is to be used on production. It is left to the reader. 

### Result
The final project can be found on GitHub [here](https://github.com/TroelsMortensen/JwtAuth)