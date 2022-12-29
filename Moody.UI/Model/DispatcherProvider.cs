using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Moody.UI.Contracts;

namespace Moody.UI.Model;

public class DispatcherProvider : IDispatcherProvider
{
    private Dispatcher? _currentDispatcher;
    public Dispatcher Dispatcher
    {
        get
        {
            if (_currentDispatcher == null)
            {
                throw new InvalidOperationException("Dispatcher is not initialized!");
            }

            return _currentDispatcher;
        }
    }

    public async Task Initialize()
    {
        TaskCompletionSource dispatcherReadyCompletionSource = new();
        
        Thread dispatcherThread = new(InitializeDispatcher(dispatcherReadyCompletionSource))
        {
            Name = "DispatcherThread"
        };
        dispatcherThread.SetApartmentState(ApartmentState.STA);
        dispatcherThread.Start();

        await dispatcherReadyCompletionSource.Task;
    }

    private ThreadStart InitializeDispatcher(TaskCompletionSource dispatcherReadyCompletionSource)
    {
        return () =>
        {
            try
            {
                _currentDispatcher = Dispatcher.CurrentDispatcher;
                dispatcherReadyCompletionSource.SetResult();
                try
                {
                    Dispatcher.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            catch (Exception e)
            {
                dispatcherReadyCompletionSource.TrySetException(e);
            }
        };
    }
}