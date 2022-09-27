# Architecture

In the previous tutorial, we applied a clean-like approach to the server. 
It does not make sense to do the same on the client side, as it has a different purpose, and is better built in a slightly different approach.

We will have two layers, one of which may be considered two in it-self.

This is the plan:

![img.png](Resources/ClientArchitecture.png)

The "UI" layer is a Blazor WASM app, and the "Clients" layer is a library with classes, which can make HTTP requests to the Web API.

The "UI" layer has a "sub-layer" marked "View models". That is because they are often tightly intertwined with the UI. I.e. the View models are not a separate layer or component.

Now, we will need at least two new components: one per layer. That leaves the interfaces, where to put them?\
Sometimes you might see people put them in their own component, e.g. called Contracts. However, in our case that might be a bit excessive.

We will revisit the consideration from the clean approach, giving the following discussion: 

Put the interfaces in the component least likely to be swapped out. In this case here, the Clients are swapped out, if we changed network technology, i.e. we move from REST to e.g. gRPC or GraphQL or something else.\
The UI layer may be swapped out, if we don't like Blazor-WASM.  

If the interfaces were put in Clients, it would be easier to reuse that component if we were to add other types of client apps: desktop or mobile apps. 

When starting the app, we will have to register client services, to be injected into the Blazor pages. 
This means the UI component must depend on the Clients component. 
If the interfaces are located in the UI, then the Clients component must depend on the UI, to implement the interfaces. 
This causes an impossible bidirectional dependency.

So, we cannot put the interfaces in the UI. 
They must go into the Clients, or into a separate component. 
The latter is perhaps more flexible, if we expect to add other client apps, but we don't. 
We will keep it simple, and put the interfaces in Clients.

This leads to the component/package diagram.

## Component Diagram

Below is a rough component diagram of the Client app.

![img.png](Resources/ClientComponentDiagram.png)

Each grey container is a component. The Domain already exists in your solution, it was created in part 1 of the Tutorial.

The light blue containers are directories. Not all directories are shown, just the most important ones.

## Class Diagram

The final system is displayed in the below class diagram.

You may notice, I have forgotten to suffix some methods with async, e.g. in `IUserService`, and several pages. It is super important to fix, so that will not happen. That a method is asynchronous is also implied by the return type of Task.

The Domain component already exists, we created this in the previous tutorial. But I include it here as well, because the Client app also uses it: It is a shared library.

![](Resources/ClassDiagram.svg)

