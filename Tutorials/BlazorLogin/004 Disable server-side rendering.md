# Disable server-side pre-rendering

Blazor-server has two render modes:
* Server-Render is where the component should be rendered interactively by the Blazor Server app.
* ServerPrerendered Statically prerender the component along with a marker to indicate the component should later be rendered interactively by the Blazor Server app.

As explained [here](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0-preview-9/).

It's not particularly important for you to understand the differences. But we must change the render mode from *ServerPrerendered* to *Server*. 
This is because javascript is only available, when we do not prerender, and we need this later.

Open the file Pages/_Host.cshtml.

Modify the following:

![img_3.png](img_3.png)

If we do not do this, later, we will get an expception about javascript interop not being available.
