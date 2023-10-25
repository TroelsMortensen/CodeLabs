# The End

We have now finished swapping out the File data storage with a different component using EFC.

Mostly, it was pretty painless, we didn't have to modify much in the existing code. There were some unforeseen, unfortunate necessary changes, but I hope you can still see the purpose of the Clean Architecture approach.

So far we have done integration testing, just using the Web API. You should now expand the testing to scenario testing, i.e. use the Blazor app to test whether the swapped out functionality works.

# What did I learn?
I too have learned something along the way. I have actually created this tutorial at least three times now, each time applying knowledge from the previous.
And each learning something I would include in the next version, if I ever get to that. It's comprehensive work.

The clean-architecture approach has taken a lot more focus in this latest version.
And there are things, which could be done differently. But... I removed the discussion, it was out of scope.
