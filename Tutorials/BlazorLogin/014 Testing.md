# Testing
We can now test the login functionality, and verify everything works as expected.

Run your blazor app. When it's open, go to the uri field, to append "/Login" and thereby manually navigate to the login page:

![img_11.png](img_11.png)

That should take you to the login page.

Type in credentials for one of your users, if you just copied mine, you can try 
1) Username: "Troels"
2) Password: "Troels1234"

Click Log in button, which should take you to the home page.

You can then manually navigate back to the Login page again, but because you are logged in, it should now just display this:

![img_12.png](img_12.png)

So far, so good.

But we can still not easily navigate to the Login page, and we cannot log out again. That's the next step.
