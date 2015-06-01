# ReactiveUi-AsyncTaskException-Issue
Sample that shows ReactiveUI CreateAsyncTask exceptions being swallowed

wpf sample app has a view model with two different commands.
* one created by CreateAsyncTask
* one created by CreateAsyncObservable

the app also has 5 buttons
* two that are bound using the BindCommand RUI command
* two that execute the commands on the Click event
* one that ExecuteAsyncTask() the Task-based command  on the Click event

the results are
* TaskCommand button bound - no exception caught - button now unavailable
* ObserveCommand button bound - user error caught - button continues to be available
* TaskCommand execute on click - no exception caught
* TaskCommand executeAsyncTask on click - unhandled exception caught
* ObserveCommand button bound - user error caught





