# Filtering data problem
As mentioned on the previous slide, the "simple" approach was used over the "better" approach.

It is not a problem in this tiny toy example, but it is not scalable. Here is the problem.

When the `Todos.razor` page is shown, initially we fetch the data to be shown, in the following method:

```csharp
protected override async Task OnInitializedAsync()
{
    allTodos = await TodoHome.GetAsync();
    todosToShow = allTodos;

```

I.e. we load _all_ todos. If this application were to be used by many users, there could be thousands of todos, and it is not efficient to load that many.
We are essentially loading the entire database.

Often, it is better to just fetch what is needed. Most websites will not show you everything initially, you have to search for it.

We could do something similar here, by not loading anything initially, but instead have a search button to load todos based on the search parameters.

We could also filter by who is logged in, if we added that kind of feature.

We will not dig deeper into the problem here, other than saying it is generally a bad idea to just load the entire database. It does not scale very well.
