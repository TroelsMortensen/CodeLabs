# Project structure
You have previously heard about layered systems on second semester, a simple example could look like this:

![img.png](SimpleArch.png)

The above would be a basic structure for a local desktop app. In SEP2 you expanded to a client/server system, using JFX. Such a system could be diagrammed like below:

![img_1.png](CSArch.png)

The same approach applies: Layers have different responsibilites, and each layer is separated by interfaces. In SEP2 you probably divided layers into different packages.
The next step is to separate into *components*. In Java it's called a module, in .NET it's called a *project*. This means your .NET app will be structured using an approach like below:

![](FinalAppStructure.png)

This is your entire solution. You will have at least 4 components: Blazor, WebAPI, Domain, DataAccess. Each component may contain multiple layers.  
In this first part we will start with Blazor and DataAccess. You may notice there is no component for business logic, simply because this app is rather simple. The final structure of your Todo app at the end of the semester may look slightly different. 

In your SEP3 you're going to need a component for logic.  

The point is that these projects (components) are separated, each handling their own responsibilities. There are different approaches on how to structure these components. We will do by layer, because that is simpler. However, in your professional career, you will probably encounter a separation by feature. This is the recommended approach, however much more complicated. 

This may seem a bit overwhelming, but we will take it step by step, holding hands along the way. You will be safe.