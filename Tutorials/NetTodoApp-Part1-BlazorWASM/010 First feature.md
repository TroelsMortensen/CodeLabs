# First Feature, Create User
We are ready to start our first feature:

> As a User I can add a new User, so that Todos can be assigned to Users.
 
Okay, a user can create a user, sounds a bit funky. We have the user interacting with the system, and we have information about users in the system. 

Now, this is a Todo app, so we might consider whether adding a Todo item should be the most essential, and therefore be developed first. However, when creating a Todo it should be assigned to a User, so we do the User stuff first. Otherwise we would have to go back and revise a finished feature. That's doable, but I don't want to.

We have no login-system, so everyone can create new users in the system.\
The idea is just that a Todo item is assigned to someone.

We will approach this as Domain Driven Design, i.e. start with the logic of how adding a Todo should work, and not care about where the data comes from (the Web API) or how the data is stored (the file).

## Logic
So, we will start with the application layer, where the domain logic resides. As previously mentioned, the network and data storage are just details. They come later. But the domain logic can be unit tested and verified.

What is involved in creating a new user? Well, we don't have that much data about a user, and few rules, it is going to be fairly simple.

### GitHub
This feature is located in the branch [002_AddUser](https://github.com/TroelsMortensen/WasmTodo/tree/002_AddUser).