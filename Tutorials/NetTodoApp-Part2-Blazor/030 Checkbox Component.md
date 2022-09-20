# Making a Checkbox Component

Blazor is relying on a component structure. Here we talk about Blazor-components, not the components mentioned in the discussion of architecture, i.e. the projects in your solution.

You don't strictly need components, we have not used them so far, but they can provide various benefits.

Various UI elements can be made into components, so that they can be reused, without you having to copy-paste code. This can be very convenient.

Consider the button we used in both CreateTodo and CreateUser. It looked the same, it could have the same functionality (with the disabling). Currently we have duplicated the HTML and code, but we could make the button into a component, and have the HTML, code, styling, and behaviour in just one place.

You can create your own components. You can also import libraries of components made by others. Here are some popular ones, if you are curious.

* Blazor fluent UI, found [here](https://www.blazorfluentui.net/)
* Radzen, found [here](https://www.radzen.com/)
* Syncfusion, found [here](https://www.syncfusion.com/blazor-components)
* Blazored, found [here](https://giters.com/Blazored)
* AntBlazor, found [here](https://antblazor.com/en-US/)
* MudBlazor, found [here](https://mudblazor.com/)

We will make a component now, just a simple check box, and then we will use that in the next feature.