
// See https://aka.ms/new-console-template for more information

using System.Windows.Threading;

// await Task.Run( async () =>
{
    DispatcherSynchronizationContext dispatcherSynchronizationContext = new DispatcherSynchronizationContext();
    SynchronizationContext.SetSynchronizationContext(dispatcherSynchronizationContext);
    Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;

    Thread.CurrentThread.Name = "Test";
    
    currentDispatcher.InvokeAsync(async () =>
    {
        try
        {
            await Task.Delay(1000);
            Console.WriteLine($"{Thread.CurrentThread.Name}");
            DoSomeThingAsync(dispatcherSynchronizationContext);
            await Task.Delay(5000);
            Console.WriteLine("Done");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    });
    
    Dispatcher.Run();
}


Console.ReadLine();


static async void DoSomeThingAsync(DispatcherSynchronizationContext dispatcherSynchronizationContext)
{
    await Task.Delay(2000);
    SynchronizationContext.SetSynchronizationContext(dispatcherSynchronizationContext);
    Console.WriteLine("Passed Delay!");
    throw new InvalidOperationException($"{Thread.CurrentThread.Name}");
}