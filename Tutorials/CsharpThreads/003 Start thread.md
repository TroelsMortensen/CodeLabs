# Starting a thread, and printing numbers

In Java the `Thread` constructor takes an instance of a class, which implements the interface `Runnable`.  
In C# the `Thread` constructor takes a delegate, i.e. either a lambda expression, or a method reference.

As an example, we would like to execute the following method in a new thread:
```csharp
public void PrintNumbers()
{
    for (int i = 0; i < 1000; i++)
    {
        Console.WriteLine(i);
        Thread.Sleep(100);
    }
}
```
This method just prints out numbers from 0 to 999. Notice the `Thread.Sleep(100)` method call, which sleeps the *current* thread for 100 milliseconds. Identical to the `sleep()` method in Java.

Now, we would like to create a thread to execute this method in a new thread. That can be done as follows:
```csharp
Thread thread = new Thread(PrintNumbers);
thread.Start();
```
A new thread instance is created, the constructor takes a method reference to the `PrintNumbers` method shown above.  
When the `Start()` method is called on the `thread` a new thread is created, and the method is executed.