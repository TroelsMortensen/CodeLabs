# Testing the Seeding

Now, we have something we can test. Nothing in the DAO class works, the methods are not yet implemented, but we can run the Web API and manually verify that the database received the seeding data.

When you can run the Web API without errors:

1) Run the Web API
2) Once it has started up and is ready, just terminate it again (if you try to use swagger, you'll just get errors, as no DAO methods are implemented)
3) Open the Database window in Rider
4) Double click on the Todos table
5) You should see your five Todo items in the table 

