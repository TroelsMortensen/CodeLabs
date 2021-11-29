# Client Side
This is the client side of the example. It sends a string to the server, and receives a string back again.  
You will notice similarities to the server with regards to reading and writing data.

Below is a main method with the client program:

```csharp
class Program
{
    static void Main(string[] args)
    {
        using TcpClient client = new TcpClient("127.0.0.1", 5000);

        using NetworkStream stream = client.GetStream();

        byte[] dataToServer = Encoding.ASCII.GetBytes("Hello from client");
        stream.Write(dataToServer, 0, dataToServer.Length);

        byte[] dataFromServer = new byte[1024];
        int bytesRead = stream.Read(dataFromServer, 0, dataFromServer.Length);
        string response = Encoding.ASCII.GetString(dataFromServer, 0, bytesRead);
        Console.WriteLine(response);
    }
}
```

**Line 5**: We instantiate a new `TcpClient`, which makes a connection to a server located at the provided IP address and port number, the two constructor arguments. Notice again the `using` keyword, so that the client object is disposed when no longer used.

**Line 7**: Obtaining the stream.

**Line 9**: The message to be sent to the server, `Hello from client`, is converted to a byte array.

**Line 10**: The byte array is sent to the server.

**Lines 12-14**: Identical to how the server side read from the client. Here we just read from the server instead.

**Line 15**: Finally, printing out the result.