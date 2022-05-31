# The Domain component
First, we need the model classes. 
In this first iteration of the tutorial, we will just need a Todo object. Later we might add Users, or multiple todo-list.

### New project (component)
Create a new Class Library project, by right-clicking your solution:

![img.png](Resources/CreateLibrary1.png)

This will open a familiar dialog, where you can create a Class Library ((1)). 
Give the project a name ((2)), I have called mine *Domain*, in some examples it is called *Entities*. 
In your case, you can probably only select net6.0 ((3)). Finally press Create ((4)).

![img.png](Resources/img.png)

A **Class Library** is a type of project which cannot be run, it instead contains functionality. 
All NuGet packages are generally libraries with functionality, you can import into your system. 
Similar to some Jar files in Java, however, other Jar files can actually be executed. In .NET we distinguish between .exe files: executables, and .dll files: dynamic link libraries.

Your Domain component will just contain the domain model classes, there is nothing to *run*. 
If you make custom Exceptions, they could also go here. We will also add certain interfaces, shortly.

This component will contain things that are needed across the application.