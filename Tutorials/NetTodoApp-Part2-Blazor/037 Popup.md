# Creating a Popup
The purpose of this functionality is to show another way of passing data to a component, i.e. using "Child Content".

Currently whenever you have successfully added a Todo, you are just being navigated to the View Todos page. A user might be confused, because no success message was displayed first.

We are going to create a success message shown in a popup. The popup will be a component, and the content and functionality of the popup will be arguments to the component.

There are several libraries, mentioned in step 32, which can do this, but let's start with a manual approach. There are probably smarter ways to do this, but this approach is also to reiterate how to pass arguments to components.


## UI Element Type
There are different kinds of popup-types, and these kinds of popups have some common names based on how they are shown.
We will make a modal (or sometimes called toast). There is another called snackbar, and probably more variations. It's good to know, if you need to google for examples.

## New Component
First, create a new blazor component, inside the Pages/UIComponents directory. Call it "Modal".


## The Code and View
This is the entire component code:

```razor
@namespace UIComponents

@if (ShowModal)
{
    <div class="modal-background" style="display:block; height: 100%">
        <div class="modal-box">

            @ChildContent

        </div>
    </div>
}

@code {

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;
    
    [Parameter]
    public bool ShowModal { get; set; }
}
```

We define the namespace of the component at the top, because it is in a different directory than the pages.\
Then some `<div>`s to wrap things, with various styling, see below.\

This piece of html (and css) was taken from an [example found on w3shcools](https://www.w3schools.com/howto/howto_css_modals.asp).

There isn't much here. The component is just a "wrapper", which can take some html content, and show it in a popup.

**Line 6** is important, it defines @ChildContent, and this means you can pass html/razor-syntax to a component, by setting the ChildContent. It has to be named like this, and the property must be defined as shown in the code above. How to use it will be shown later.

Everything is wrapped in an if-statement, so that the popup can be displayed or hidden. 

In the code block, we have one parameter, which will be the content to be shown in the popup.

## The Style
Next up, we need the styling. Add a style-behind. Paste in the following:

```css
.modal-background { /* This style class makes the background darker, and un-clickable */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0, 0, 0, 0.47); /* Black w/ opacity */
    display:block;
}

/* Modal Content/Box */
.modal-box {
    background-color: #fefefe;
    margin: 15% auto; /* 15% from the top and centered */
    padding: 20px;
    border: 1px solid #000000;
    /*display:inline-block;*/
    width: 30%; /* Could be more or less, depending on screen size */
    min-width: 300px;
    border-radius: 25px;
    box-shadow: 0 5px 30px 15px #3f3f3f;
}
```

Again, it's stolen from the example, linked above, you may modify it if you please.

That's the basic component. Next up, we need to use it for something.
