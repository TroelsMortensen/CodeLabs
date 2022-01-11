# Calling a method on another class
Sometimes we would like to keep the behaviour in a separate class. E.g. in Java we usually create a separate class, which implements the `Runnable` interface, and so the `run()` is called, when the thread is started. We can do something similar in C#.

Below is a class, which has a method to print numbers, like previous examples.
```csharp
public class NumberPrinterClass
{
    public void PrintNumbers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(i);
            Thread.Sleep(100);
        }
    }
}
```
Now, we would like to execute this functionality in a thread, created from a different class.  
In the below main method, a new instance of the above class is created, and then a thread, which is started.
```csharp
static void Main(string[] args)
{
    NumberPrinterClass npc = new();
    Thread thread = new Thread(() => npc.PrintNumbers(100));
    thread.Start();
}
```