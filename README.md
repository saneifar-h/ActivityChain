<h1>Activity Chain</h1>
There are many times which you want to do some actions on a object and these actions must run in a sequentially. 
Think just of application Service layer where you want to react to message and do some work like validation, calling services and saving data and....
And these task have about N ms execution time so when you have 3 actions in your handler method the total time of your execution is N*3.
So when when M users call an api which use this method the total time of execution is M*N*3.
But think we can seprate the 3 actions to three pipe that can execute actions parallel and making a piplines of this actions. 
By Some calculation we can find that although the execution time of each actions still 3N but the toal time of execute M requests will be (M+2)*N and response time of M message will be reduced.
By this library you can make a chain of actions that each link of this chain can act as a piple.
