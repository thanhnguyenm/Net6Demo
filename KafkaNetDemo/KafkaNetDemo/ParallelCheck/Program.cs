// See https://aka.ms/new-console-template for more information
try
{
    Parallel.Invoke(
        BasicAction,    // Param #0 - static method
        () =>           // Param #1 - lambda expression
                    {
            Console.WriteLine("Method=beta, Thread={0}", Thread.CurrentThread.ManagedThreadId);
        },
        delegate ()     // Param #2 - in-line delegate
                    {
            Console.WriteLine("Method=gamma, Thread={0}", Thread.CurrentThread.ManagedThreadId);
        }
    );
}
// No exception is expected in this example, but if one is still thrown from a task,
// it will be wrapped in AggregateException and propagated to the main thread.
catch (AggregateException e)
{
    Console.WriteLine("An action has thrown an exception. THIS WAS UNEXPECTED.\n{0}", e.InnerException.ToString());
}

static void BasicAction()
{
    Console.WriteLine("Method=alpha, Thread={0}", Thread.CurrentThread.ManagedThreadId);
}