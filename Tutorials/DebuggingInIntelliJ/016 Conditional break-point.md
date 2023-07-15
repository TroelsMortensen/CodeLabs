# Conditional Break-Point

Sometimes, you have a problem with a specific element in a list, and the faulty code is in a for-loop.

You go debugging, place a break-point in the for-loop, and press <kbd>Resume Program</kbd> until the correct element is active. 

If there are many elements in the list, it will take a long time to get to the correct element.\

Example: In our case, we want to inspect the calculation for "Parmesan Cheese", i.e. the fifth element in the list.\
I could put a break-point at line 17 in the Main class, i.e. the first line inside the for-loop, and just <kbd>Resume Program</kbd> until "Parmesan Cheese" product is active.\
But this could take time.

I have previously hacked this by creating an if-statement, like this:

![](HackConditionalBreakPoint.png)

It does work, it will pause execution when `product` is the "Parmesan Cheese".\
But it is not particularly pretty, and you may forget to clean up. 

It is better to create a conditional break-point.\
You do this by creating a normal break-point, and right-clicking it.\
Now you can insert a condition:

![](InsertConditionalBP.gif)