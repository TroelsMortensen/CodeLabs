# The End

We have now finished swapping out the File data storage with a different component using EFC.

Mostly, it was pretty painless, we didn't have to modify much in the existing code. There were some unforeseen, unfortunate necessary changes, but I hope you can still see the purpose of the Clean Architecture approach.

So far we have done integration testing, just using the Web API. You should now expand the testing to scenario testing, i.e. use the Blazor app to test whether the swapped out functionality works.

