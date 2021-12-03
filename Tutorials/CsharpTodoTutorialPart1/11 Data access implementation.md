# Data Access Object
We currently only have one type of domain object, so we just need a single DAO to provide CRUD operations.

This DAO should be located in the FileData component, either put it in the DataAccess directory or create a new.

![img_11.png](Resources/img_11.png)

This class should implement the `ITodoService` interface from the Blazor component, and provide implementations for the methods.

First, we need a FileContext. We will just instantiate this in the constructor:

```csharp
public class TodoFileDAO : ITodoService
{
    private FileContext fileContext;

    public TodoFileDAO(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }
    
    // ...
```
We use constructor dependency injection for the FileContext, 
so we don't have to manually create a new instance. 
This is generally a good approach. Remember SDJ2 and MVVM: 
Controllers, VMs, Models got what they needed through constructors. 
We will get the framework to handle this dependency injection for us.

