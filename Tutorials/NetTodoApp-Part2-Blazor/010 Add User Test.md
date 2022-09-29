# Test

We should now have the functionality in place. We will add a bit of styling later, but let's test it first.

Run first your Web API. You can just close the Swagger page, which opens.

Run then the BlazorWASM. It should open a new tab in your browser.

In the address bar put "https://localhost:7205/CreateUser" to navigate to the page, we have just worked on (your port may be different).

You should then see your page:

![img.png](Resources/CreateUserPageView.png)

Type in something in the text-field and see how the button becomes enabled.

Try first a user name, you know exists. We should see an error message.

Then try a new user name, non-existing. This should work, and you should see a green success message.\
If you have no users, do the above in reverse order.