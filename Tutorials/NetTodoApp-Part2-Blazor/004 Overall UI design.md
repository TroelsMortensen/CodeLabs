# UI design
We will not rework the UI very much for this tutorial. This means we will keep the left side navigation menu, and just add a few more menu items to it, as necessary.

Should a more elaborate rework be desired, the basic setup is found in BlazorWASM/Shared/MainLayout.razor

Out of the box, it looks like this:

```razor
@inherits LayoutComponentBase

<div class="page">
    <div class="sidebar">
        <NavMenu/>
    </div>

    <main>
        <div class="top-row px-4">
            <a href="https://docs.microsoft.com/aspnet/" target="_blank">About</a>
        </div>

        <article class="content px-4">
            @Body
        </article>
    </main>
</div>
```

You may notice the `<NavMenu/>` in line 5, which is the component (not project component, but Blazor component) that contains the HTML and code for the left side navigation menu.
If you remove this line, the navigation menu disappears.

Also notice the 

```razor
<div class="top-row px-4">
    <a href="https://docs.microsoft.com/aspnet/" target="_blank">About</a>
</div>
```

This is the top bar, always present. You can modify, or remove this. Some pages have navigation menus in the top panel instead of the side.

Actually, the only thing of the view, you cannot remove here, is the `@Body`. This variable is extremely important:\
Whenever you open a new page, the content of the page will be inserted at this variable.

So, if you want a very different layout of your app, this file is where to change the overall structure.

As mentioned, in this tutorial, we will not do any rework.