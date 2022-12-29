using System.Threading.Tasks;
using System.Windows;
using Moody.UI.Contracts;

namespace Moody.UI.Model;

public class WindowHost : IWindowHost
{
    private readonly IDispatcherProvider _dispatcherProvider;

    public WindowHost(IDispatcherProvider dispatcherProvider)
    {
        _dispatcherProvider = dispatcherProvider;
    }

    public async Task ShowWindow(object viewModel)
    {

        await _dispatcherProvider.Dispatcher.InvokeAsync(async () =>
        {
            Window window = new Window();
            await ShowWindow(window);
        });
    }

    public async Task ShowWindow(Window window, object viewModel)
    {
        await _dispatcherProvider.Dispatcher.InvokeAsync(() =>
        {
            window.DataContext = viewModel;
            window.ShowDialog();
            return Task.CompletedTask;
        });
    }
}