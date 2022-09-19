# Adding Styling to Create User Page

Now that the functionality works, we can beautify it just a little bit. This is not a course in HTML and CSS, but we will still look at an example of how to apply it.

Remember, we have two approaches to styling:

* App wide css is defined in a file in the wwwroot/css. You can have multiple style-sheets in here, and they are made available app wide by importing them in the wwwroot/index.html file.
* Component scoped styling, i.e. a style-sheet only available to a single component.

We will use the latter, and create a "style-behind".

Right click Pages directory, add a new style sheet. The name must be that of the component appended with ".css", in our case that will be "CreateUser.razor.css".

If done correctly, you should now see a style sheet nested under your razor component file:

![img.png](Resources/CreateUserStyleBehind.png)

Input the following into the .css file:

```css
.card {
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
    transition: 0.3s;
    width: 250px;
    padding: 25px;
    text-align: center;
    margin: auto;
    margin-top: 50px;
    border-radius: 15px;
}

.field {
    margin-top: 20px;
}

.acceptbtn {
    background-color:lightgreen;
    border-radius: 5px;
    padding: 5px;
    padding-left: 10px;
    padding-right: 10px;
}

.button-row{
    margin-top: 15px;
}
```

I will not go through the CSS, but the result should look like this:

![img.png](Resources/CreateUserPageWithStyling.png)

The functionality remains the same.

Now, we may wish all "accept"-type buttons to have the same style, in which case, we should move that style-class to the app wide style-sheets. Or we could make the button a separate component. We might come back to this 