# Multi-threading overview
The threading functionality exists in the namespace **System.Threading**

This namespace provides classes and interfaces which enable multithreaded programming, i.e. parallel execution of code, and leveraging threads.

### Example usage
* Separate heavy calculations from UI (to avoid freezes)
* Regularly query external service, and notify application if new data arrived (polling)
* To avoid stopping processing when waiting for user's input
* When you have independent tasks, which do not intersect
* Handling multiple clients in a client-server setup with sockets