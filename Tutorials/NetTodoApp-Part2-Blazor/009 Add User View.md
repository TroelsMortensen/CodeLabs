# The View

Next up, the view definition.

As a minimum we need an input field, so that the user can input the wanted user name.\
We also need a button, which when clicked, will call the `Create()` method.\
And then we need a way to display messages held in the `resultMsg`.

I have added a little extra, which will be explained, and then the view looks like this:

```razor
@page "/CreateUser"
@using Domain.DTOs
@using HttpClients.ClientInterfaces
@inject IUserService userService;

<div class="card">
    <h3>Create User</h3>
    <div class="form-group field">
        <label>User name:</label>
        <input type="text" @bind="username" @bind:event="oninput"/>
        @if (!string.IsNullOrEmpty(resultMsg))
        {
            <label style="color: @color">@resultMsg</label>
        }
    </div>
    <div class="button-row">
        <button @onclick="Create" disabled="@(string.IsNullOrEmpty(username))" class="acceptbtn">Create</button>
    </div>
</div>
```

**Line 1**: This is the page directive. It defines the sub-uri to access this specific page.\
**Line 2-3**: Importing various namespaces.\
**Line 4**: Here we inject an instance of `IUserService`.
This instance is created and provided by the dependency injection framework, and we can do this, when we have registered the interface and implementing class as services in Program.cs, as we did the previous slide.\
We hereby achieve dependency inversion, and if we were to change the network technology, the idea is, we shouldn't have to rework any of our pages, because they just know about the interface.\
However, because we decided to put the interfaces into the HttpClients component, we would still have to modify all pages, if we changed network technology. A new network implementation would most likely be placed in a different namespace.\
Notice the using statement in line 3, this references the namespace HttpClients. We have chosen to accept this flaw, as we will not change network technology. Remember the discussion on slide 2 on where to put the interfaces.

But, this page does not know about the implementation behind the interface.

Then comes a mix of HTML and razor-syntax, i.e. inlined C#.\
First there is a containing `<div>` in line 6. We will add some styling later to setup the page a little nicer.

The first interesting thing comes in **line 10**. This is a text input field:

```razor
<input type="text" @bind="username" @bind:event="oninput"/>
```

We have two "@-attributes". The first one says that the value of the text field should be stored in the field variable `username`. The binding is two-way: if the field variable is changed from the code, the view will update to show this value.

The second `@` is then the type of event, which should cause the value of the input field to be put into the field variable.
We want it to be "oninput", i.e. each key press will cause an update to the value of the field variable.\
The default is "onchange", which will cause the update when you de-focus the text input, i.e. click somewhere outside of it. If we just want the default onchange we can leave out the second `@bind:eve...`.\
We want the "oninput", because the button should be  disabled, when there is nothing in the input field.

**Line 11-14**: This is a code snippet, started with the `@` to indicate some razor-syntax follows, which should be evaluated when generating the html.\
We check if there is anything in the `resultMsg` field, and if so, we display a `<label>` with whatever error message should be shown. If the `resultMsg` is empty, the HTML inside the if-statement is not rendered.

**Line 17**: Here we have the button to be pressed when creating a user:

```csharp
<button @onclick="Create" disabled="@(string.IsNullOrEmpty(username))" class="acceptbtn">Create</button>
```

We specify the method to be called when the button is clicked with `@onclick="Create"`. We could also here provide a lambda expression instead of the method name.\
Then the `disabled=` is a standard HTML attribute, the value of which must be "true" or "false".
Here we evaluate the state with `@(string.IsNullOrEmpty(username))`, the @ indicating razor-syntax to be avaluated when rendered. We check if the `username` has a value. If there is no value, it doesn't make sense to be able to click the button, so it will be disabled.
