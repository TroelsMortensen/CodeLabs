# Popup component

We will continue to play a bit with components, this time we will create a popup to show messages (and other stuff).

There are several libraries, mentioned in step 32, which can do this, but let's start with a manual approach.
There are probably smarter ways to do this, but this approach is also to reiterate how to pass arguments to components.

We can start by using it to show a confirmation message to the user, when a new Todo was added successfully.

The result will look like this:

![img.png](Resources/ModalExample.png)

You may style the button, if you wish.

### Setup
In the UIElements directory, create a new blazor component called `Modal.razor`. 

These kinds of popups have some common names based on how they are shown.\
We will make a modal (or sometimes called toast). 
There is another called snackbar, and probably more variations.
It's good to know, if you need to google for examples.

### Code
The entire component looks like this:

```razor
@namespace UIElements

<div class="modal-background" style="display:block; height: 100%">
    <div class="modal-box">
    
        @ChildContent
    
    </div>
</div>

@code {

    [Parameter]
    public RenderFragment ChildContent { get; set; }

}
```

First, at the very top, we define the namespace.

Next, two nested `<div>` tags to organize everything. 
This was taken from an [example found on w3shcools](https://www.w3schools.com/howto/howto_css_modals.asp).

There isn't much here. The component is just a "wrapper", which can take some html content, and show it in a popup.

**Line 6** is important, it defines `@ChildContent`, and this means you can pass html/razor-syntax to a component, by setting the `ChildContent`.
It has to be named like this, and the property must be defined as shown in the code above. How to use it will be shown later.

In the code block, we have various parameters.

The RenderFragment in line 37 is, as mentioned, some other html/razor-syntax.\
The Title is obviously the title.\
We have the text on the accept button, a message to put into the errorLabel if needed.\
And then two CallBacks, on for pressing the <kbd>x</kbd> to close the modal, and another for what should happen if accept-button is pressed. Again, we could add a cancel button, if needed. Maybe in a later example.

Let us try it out, se if we can make it work.

#### Note
The above component-html can be simplified. For example, [this](https://www.w3schools.com/howto/howto_css_modals.asp) link also shows a modal, the html of which could be used instead of the above.
I do believe this example will also grey out the background, which is a nice detail. At some point, I may update the tutorial.
