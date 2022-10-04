# Architecture

We are going to add a component to house the functionality of using Entity Framework Core for data management.

Below is a low detailed class diagram, you saw it back in part 1:

![](Resources/LowDetailedClassDiagram.svg)

You don't currently have the EfcDataAccess component, but we will make it shortly.

We put the I*Dao interfaces into the Application component to make it easier to swap out the implementations, and we will see this in a minute.\
This is where [Dependency Inversion Principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle) and [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection) comes into play.

