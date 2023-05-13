using System;
using System.Threading.Tasks;
using Moody.UI.Contracts;

namespace Moody.UI.Command;

public class AsyncCommandFactory : IAsyncCommandFactory
{
    private readonly IDispatcherProvider _dispatcherProvider;

    public AsyncCommandFactory(IDispatcherProvider dispatcherProvider)
    {
        _dispatcherProvider = dispatcherProvider;
    }

    public AsyncCommand Create(Func<object?, Task> executeAsync)
    {
        return new AsyncCommand(executeAsync, o => Task.FromResult(true), _dispatcherProvider);
    }
    
    public AsyncCommand Create(Func<object?, Task> executeAsync, Func<object?, Task<bool>> canExecuteAsync)
    {
        return new AsyncCommand(executeAsync, canExecuteAsync, _dispatcherProvider);
    }
}