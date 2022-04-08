# Adding Dependencies

We are going to use Entity Framework Core and SQLite, so we are going to have to add some NuGet packages.

## NuGet Manager

Open the NuGet package manager:

![img_3.png](img_3.png)

This should show a window in the bottom half of Rider.

Alternatively, there should be a NuGet button in the bottom row of Rider.

## Packages

Once the window is open, you need to add 3 packages (see how further below):

* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Sqlite
 
![](AddPackage1.gif)

Notice the progress bar at the bottom after accepting installation.

Pick the latest version which matches your .NET version. E.g. if you're on .NET6, pick version 6.x.x.\
Don't pick the preview versions.

For all packages, the version should be the same.

## Verify

You should be able to verify the installed packages and versions:

![img_5.png](img_5.png)

## Internal Dependency

We have added external packages. We also need an internal dependecy: EfcData -> Domain. We need access to the model classes and interfaces in the Domain component.\
Add this reference.