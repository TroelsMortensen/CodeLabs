# Edit Todo

This isn't exactly a user story. However, we have the Web API endpoint which can update a Todo, so let's make the front end part of that functionality.\
[You can find the code in this branch](https://github.com/TroelsMortensen/WasmTodo/tree/016_EditTodo)

Furthermore, this will show a way to pass arguments to a page.

For the View Todos page, we would potentially select some filters, and ask to load the data with the click of a button.\
Sometimes you want the page to auto-load something, but what that something is may vary.

Imaging this: Your Todo object has many more properties than the current version. The View Todos will provide an overview, but not show details for each Todo.\
To see details, we would click on a row in the table, or an icon in a row, to "view details".\
We would be taken to a different page, and that page should automatically display details for the Todo we selected.\
We don't want to have to search for a specific Todo on this next page.

This is a common case, you will often need, so we include the feature here.

It requires a new page, and modifications to the ViewTodos.razor.

We already have a method in ITodoService to support this feature: ITodoService::UpdateAsync.\
We will need another method to Get a single Todo, by Id.
