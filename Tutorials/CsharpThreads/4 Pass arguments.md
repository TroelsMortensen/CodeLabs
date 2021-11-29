 # Passing arguments

In the previous example, we just referenced a method. This can be done, when we don't need to pass any arguments.

The below method also prints numbers, but requires an argument.
```csharp
public void PrintNumbers(int count)
{
    for (int i = 0; i < count; i++)
    {
        Console.WriteLine(i);
        Thread.Sleep(100);
    }
}
```

The way we start the thread now is to use a lambda expression, like below:
```csharp
Thread thread = new Thread( () => PrintNumbers(1000) );
thread.Start();
```
This lambda expression is an anonymous method.  
`() => PrintNumbers(1000)`  
The leading `()` defines the arguments to be passed to the method. In this case, there are none, so the parameter list is empty.
The `=>` is the lambda operator, so the right hand side is the functionality to execute, in this case it's a method call to `PrintNumbers`.