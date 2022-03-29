# Can we add a Todo?

Now is the time to test, if we can add a todo.

Navigate to the page. Without putting anything in either field, press the "Create" button, and verify you get an error message.

![img.png](Resources/ErrorMsg.png)

The red outlines are to indicate where the input errors are. The text at the top comes from the component `<ValidationSummary/>`, 
which was added almost at the top of the `<EditForm>`.

Next:
* Type in a User Id larger than 0
* And put in a title

Press the "Create" button.

You should be taken to the "Todos" page, and at the bottom row, your todo is shown. Notice the `TodoID` has been set correctly.