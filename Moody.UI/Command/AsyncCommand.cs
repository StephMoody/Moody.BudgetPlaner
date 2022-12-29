using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Moody.UI.Contracts;

namespace Moody.UI.Command;

public class AsyncCommand : ICommand
{
    private readonly Func<object?, Task> _executeAsync;
    private readonly Func<object?, Task<bool>> _canExecuteAsync;
    private readonly IDispatcherProvider _dispatcherProvider;
    private bool _canExecute;

    public AsyncCommand(Func<object?, Task> executeAsync,
        Func<object?, Task<bool>> canExecuteAsync,
        IDispatcherProvider dispatcherProvider)
    {
        _executeAsync = executeAsync;
        _canExecuteAsync = canExecuteAsync;
        _dispatcherProvider = dispatcherProvider;
    }
    
    public bool CanExecute(object? parameter)
    {
        _dispatcherProvider.Dispatcher.InvokeAsync(async () =>
        {
            try
            {
                bool oldCanExecute = _canExecute;
                _canExecute = await _canExecuteAsync(parameter);
                if (oldCanExecute == _canExecute)
                    return;

                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        
        });
        return _canExecute;
    }

    public void Execute(object? parameter)
    {
        _dispatcherProvider.Dispatcher.InvokeAsync(async () =>
        {
            try
            {
                await _executeAsync(parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }

    public event EventHandler? CanExecuteChanged;
}