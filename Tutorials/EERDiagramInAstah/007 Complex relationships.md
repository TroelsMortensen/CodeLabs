# Complex relationships

Sometimes 3 or more entities are involved in a relationship, called a complex relationship (or ternary relationship, if there are 3 entities).

This is represented with a diamond.

In Astah this is not possible out of the box, so we need to set up some things.

### Setup

First, we need an icon for the diamond. I have just drawn the below in Inkscape:

![img](Ternary_Icon.png)

You should be able to right-click the image and download. Or create your own.

Then the settings.

Open the menu: Tools -> Project Settings -> Set Icon for Stereotype:

![img_10.png](img_10.png)

In this menu, go to Profile Stereotype ((1)) and create a new ((2)):

![img_12.png](img_12.png)

Then double click on the new stereotype ((3)).

In this menu, change the Stereotype name ((1)), and click on the "..." ((2)) to select an icon.

![img_11.png](img_11.png)

Select the diamond image.

![img_13.png](img_13.png)

Click <kbd>OK</kbd>, then <kbd>Close</kbd>

That's the setup.

### Creating the relationship

Create a new class, where the diamond should be, the name of the class is the name of the relationship.\
In this example a _doctor_ **treats** a _patient_ for a _disease_. I.e. **treats** is the relationship.

![img_14.png](img_14.png)

Select the **treats** class. In the left side menu, there is a Stereotype tab ((1)), open it. 
Add a new stereotype ((2)), and call it "complex" ((3)), like the Stereotype icon name above.

![img_15.png](img_15.png)

Right-click the **treats** class and select "Set Customized Icon". 
This shows a popup, double click on the diamond image.

Your **treats** class should now look like the diamond image. 

![](ConvertToDiamond.gif)

Then change the diamond size, and draw relationships. 

![](ScaleAndDrawRelationships.gif)

The name of the complex relationship is now below the diamond. With a dirty hack, we can move the name inside the diamond.

### Name in diamond

Double click on the name, "treats", so it's editable:

![img_22.png](img_22.png)

Then replace the name with a white space " ".

After that, use the Text tool to create a text label inside the diamond.

![img_23.png](img_23.png)


Final result: 

![img_24.png](img_24.png)