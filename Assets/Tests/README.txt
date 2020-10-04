Welcome to the unit tests folder. 

Some things you should know about the framework here:

Inside the PlayMode folder, the folder 'BuildPlayerScripts' contains implementations of a few interfaces that are
provided as part of the Unity Testing Framework.


The single method in this file is responsible for changing the behavior of the building of the test player for PlayMode tests. The player options lets you specify things such as-

Unity scenes to include in the assembly.
Allowing to target a different machine for the player to be built on, versus the machine where the tests are run.
 

