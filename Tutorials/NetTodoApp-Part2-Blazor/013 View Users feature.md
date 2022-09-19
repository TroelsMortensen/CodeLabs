# View Users
The second user story is:

> As a User I can get a list of all existing Users, so that I can assign Todos to them

Now, the point of this one is actually just to be able to assign users to Todos, so the phrasing of the user story could have been better. But we needed that endpoint in the Web API, to retrieve a list of users.

So, we might as well make a page to view all users.

## New Page
Make a new page in the Pages directory, call it "ViewUsers".

The content will look like this:

```razor
@page "/ViewUsers"
<h3>ViewUsers</h3>

@code {
    
}
```


## Add to menu
Let's also add it as a menu Item right away. Go ahead and do that, in the same way as the previous page.

Notice that for each nav menu item in the HTML of NavMenu.razor, there is a `<span>` tag, with the class defining what icon should be next to the text.

[You can find many available icons here](https://icon-sets.iconify.design/oi/), they seem to be included in bootstrap, located in a file here: BlazorWASM/wwwroot/open-iconic/font/css/open-iconic-bootstrap.min.css.

So, maybe the icon for the menu item should be a list. Or something else. You can pick whatever.