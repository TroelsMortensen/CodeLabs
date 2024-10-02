# Adding packages to the project
I assume you have a project, probably a class libary, that you want to add EFC to.

## Open NuGet package manager

Open NuGet package manager, either:
* Alt + Shift + 7, or
* Menu: View -> Tool windows -> NuGet, or
* Click the NuGet icon on the lower left icon bar.

## Add packages
You must add the following packages to your project:
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design

Then you need a specific package for your database provider. For example, if you use SQLite, add:
* Microsoft.EntityFrameworkCore.Sqlite

If you want to use Postgres, that's also possible. You need to google the package name for that.