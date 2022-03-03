# Status

Now you have been through the very basics of Blazor.

* You have seen how to mix html and razor-syntax to dynamically render pages.
* You have seen how to use data-binding to dynamically display information on the pages.
* You have seen how to add event-listeners, so methods are called e.g. when clicking a button, or inserting text in a text field.

These are the basic elements, and you can make quite fancy apps with just this.

The next topic is more advanced, it is about how to use components for re-usability in your app, and better code-organization/structure. 
Use of components are not strictly necessary, you can get far without, but they make many things much easier. 

For example, some times pages can become quite large, and it is beneficial to be able to split a page into smaller parts, in separate files.
This can really help with the organization. For this, we can use components.

### Class Diagram
Currently, our app looks like what is shown below. 
Please note how pages, components, and properties are denoted.

Also note the dotted arrows between components (green boxes), to indicate component dependencies. We see here that:
* Blazor depends on Domain, and FileData
* FileData depends on Domain
* Domain depends on nothing



![](Resources/ClassDiagram.svg)

Components look like packages, however, the name is in a different place, and the little "tab" at the top left has an upside-down fork icon.

Pages are marked with the stereotype `<<Page>>`, and components are similarly marked with the stereotype `<<Component>>`.

Properties are public, and also get a stereotype `<<prop>>`. I did not spell out `<<property>>` for brevity.

Personally, I like to color code things, in this case different colors for components, packages, pages/classes, components and interfaces.\
If I need to have packages inside other packages, each new depth of package, will get a new color. I.e. all current packages are level 1, they are directly in a component.
Were I to make a new package inside, e.g., Pages, that new package would be a level 2, and I would give level 2 packages a slightly different color of yellow. Maybe darker for each level.

As there is little to no business logic in the app, there is no layer for this functionality. Should we need it, we could introduce a layer in the Domain component, e.g.

```
----------------------------
| AddTodo, EditTodo, Todos |
----------------------------
|       <<ITodoHome>>      |
|          Logic           |
|        <<TodoDAO>>       |
----------------------------
|        TodoFileDAO       |
----------------------------
```

### Next up

The next part will include more advanced ways of structuring your Blazor application, using components.