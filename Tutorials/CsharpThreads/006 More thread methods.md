# More thread methods
Similar to Java, we have a couple of methods available for the threads. Relevant methods are:
* Thread.sleep(milliseconds : long)
* thread.Join(otherThread) - to tell a thread to not start until the argument thread object is finished. This will make the threads run in serial.
* thread.Interrupt() - to wake up a sleeping thread. It throws an exception, which must be handled.
