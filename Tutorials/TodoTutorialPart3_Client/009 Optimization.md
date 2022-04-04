# Optimization

Currently, when you open the Todos overview in the Blazor app, all Todos are initially loaded.

This does not scale well. If you have thousands, or more, Todos in your data storage, the initial load will be very large.

The next step will rework this. We introduce a search button, so nothing is initially loaded, but instead only per the request of the user.

The user may choose to load everything, or apply various filters before loading data.

