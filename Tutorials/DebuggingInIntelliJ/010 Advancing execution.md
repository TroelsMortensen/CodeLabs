# Advancing Code Execution

During debugging we often step through the program one line at a time to examine the program state at various execution points. This is the bread and butter of debuggin.\
There are two main ways you can step through the code:

1) `Step Over` <kbd>F8</kbd> : if you're not interested in the method, the debugger will execute the call and stop at the next statement after the method call.
2) `Step Into` <kbd>F7</kbd> : if paused at a line of code which calls a method, you will step inside the method and stop at the beginning of this method.

![](StepButtons.png)

We'll take a look at both shortly.