# File data component
Create a new project again, this time a library:

![img_5.png](Resources/img_5.png)

Again, delete the Class1.cs.

We then need to add dependencies to other components, so that the FileData component can use classes from other components.
Inside the FileData component, right-click the Dependencies:

![img_7.png](Resources/img_7.png)

Then select the Domain component ((1)), and click Add ((2)).

![img_8.png](Resources/img_8.png)

This means your FileData component can now access namespaces and classes in the Domain component.