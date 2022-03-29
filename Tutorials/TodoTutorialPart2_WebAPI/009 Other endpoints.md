# The Other Endpoints

It's now up to you to implement the rest of the endpoints. Below you'll find a list, with a few instructions:

### Get By Id
This endpoint must return a specific Todo object, found by its id. 

The Id must be provided as a route-parameter. That means a GET request to e.g. **"/todos/3"** will return Todo item with id=3.

### Delete By Id
This endpoint will delete a specific Todo item, based on the provided id. It is a DELETE request.

The id must be provided as a route-parameter. 

### Update
This endpoint must receive a Todo object as an argument. It is a PATCH request.

### Test

Remember to test your endpoints. And look up on GitHub if you get stuck.

### The End

This concludes this tutorial. The next part will add a Client to the Blazor app, so that Blazor can get data from the WebAPI. Eventually the File-storage will be swapped out for an SQLite database, using Entity Framework Core.