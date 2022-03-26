# Queriable Endpoint to get Todos

In the page which has an overview of the Todos, in table form, you are probably loading all Todos when the page is opened.

In this small example, that can work, but it does not scale well, if many, many Todos are added to the system.

## GET with queries
Create a new GET endpoint for todos (or modify the existing). You should now accept parameters, which can be used to filter the Todos returned to the client.

Filtering may be:
* By user id
* By completed status

Other things, you invent. You could add a "DateCreated" to the Todo object, and make it queriable by data: Get everything older than/newer than some date.

This endpoint must now accept `[FromQuery]` arguments in the method signature. You must either create a method for filtering in the DAO class, or do the filtering in the Controller.