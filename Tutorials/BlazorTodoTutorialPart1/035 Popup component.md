# Popup component

We will continue to play a bit with components, this time we will create a popup to show messages.

There are several libraries, mentioned in step 32, which can do this, but let's start with a manual approach.
There are smarter ways to do this, but this approach is also to reiterate how to pass arguments to components.

It is a bit more complicated and uses some Bootstrap style classes.

We can start by using it to show a confirmation message to the user, when a new Todo was added successfully.

The result will look like this:

![img.png](Resources/ModalExample.png)

### Setup
In the UIElements directory, create a new blazor component called `Modal.razor`. 
These kinds of popups have some common names based on how they popup, and various side-effects. 
We will make a modal (or sometimes called toast). There is another called snackbar, and probably more.
It's good to know, if you need to google for examples.

### Code
The entire component looks like this:

```razor
@namespace UIElements

<div class="modal" tabindex="-1" style="display:block" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">

                <h3 class="modal-title">@Title</h3>
                <button type="button" class="close" @onclick="() => OnCloseButton.InvokeAsync()">
                    <span aria-hidden="true">X</span>
                </button>

            </div>
            <div class="modal-body">

                @ChildContent

            </div>
            <div class="modal-footer">

                <button class="btn btn-block btn-info" @onclick="() => OnAcceptButton.InvokeAsync()" data-dismiss="modal">@AcceptButtonText</button>

                @if (!string.IsNullOrEmpty(errorLabel))
                {
                    <label style="color:red">
                        @errorLabel
                    </label>
                }
            </div>
        </div>
    </div>
</div>

@code {

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string AcceptButtonText { get; set; }

    [Parameter]
    public string errorLabel { get; set; }

    [Parameter]
    public EventCallback OnCloseButton { get; set; }

    [Parameter]
    public EventCallback OnAcceptButton { get; set; }

}
```

First, at the very top, we define the namespace.

Next, a bunch of `<div>` tags to organize everything. This was taken from an example, but I lost the source, so I'm not entirely sure what is going on.

There are some important parts, though:

**Lines 6-13** defines a header of the popup. 
You may notice in the `<h3>` tag we have the `@Title`, i.e. the title of this Toast, which is provided as a `[Parameter]`.\
In the header we also have a button, which invokes a CallBack.

**Line 16** is important, it defines `@ChildContent`, and this means you can pass html/razor-syntax to a component, by setting the `ChildContent`.
It has to be named like this, and the property must be defined as shown in the code above. How to use it will be shown later.

**Lines 19-29** defines a footer.

**Line 21** defines another button, which will _accept_ whatever. If you need a cancel button, that could be placed here as well.

**Lines 23-28** will show some error, if needed.

In the code block, we have various parameters.

The RenderFragment in line 37 is, as mentioned, some other html/razor-syntax.\
The Title is obviously the title.\
We have the text on the accept button, a message to put into the errorLabel if needed.\
And then two CallBacks, on for pressing the <kbd>x</kbd> to close the modal, and another for what should happen if accept-button is pressed. Again, we could add a cancel button, if needed. Maybe in a later example.

Let us try it out, se if we can make it work.

#### Note
The above component-html can be simplified. For example, [this](https://www.w3schools.com/howto/howto_css_modals.asp) link also shows a modal, the html of which could be used instead of the above.
I do believe this example will also grey out the background, which is a nice detail. At some point, I may update the tutorial.
