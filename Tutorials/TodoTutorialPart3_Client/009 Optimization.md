# Optimization

Currently, when you open the Todos overview in the Blazor app, all Todos are initially loaded.

This does not scale well. If you have thousands, or more, Todos in your data storage, the initial load will be very large.

The next step will rework this. The idea is to introduce a search button, so nothing is initially loaded, but instead only per the request of the user, i.e. on button click.

The user may choose to load everything, or apply various filters before loading data.

Your task is then to implement this idea:

* Nothing is initially loaded
* Introduce a button
* Search parameters (if any) are sent to the Web API
* Web API is now responsible for returning the (filtered, if applicable) result
