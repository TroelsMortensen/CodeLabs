# Testing Strategies

I will for each configuration case provide one or two test cases, to show how to test the configuration.

This is generally not as thorough as one might wish. You will need to define success and failure cases.

* How do you prove it works as expected?
* What happens if some value is missing? What should happen? Is the database constraints correct?
* What happens if a list is empty?
* What happens if one entity is deleted? Are associated entities deleted as well? Should they be?
* Is the ON DELETE behaviour "set null" or "cascade" or "restrict" or "no action"? And does this actually work?