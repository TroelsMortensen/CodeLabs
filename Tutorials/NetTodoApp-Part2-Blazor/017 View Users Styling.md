# Styling the View Users Page

Create a style-behind again, similar to what we did with the AddUser page.

Insert the below styling:

```css
.user-card {
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
    display: inline-block;
    margin: 25px;
    padding: 15px;
    border-radius: 10px;
}
```

## Test

You might want to add a couple of users first, so you have a handful.

Run Web API and then run Blazor.

Then open the ViewUsers page, it should look something like this:

![img.png](Resources/ViewUsersPage.png)