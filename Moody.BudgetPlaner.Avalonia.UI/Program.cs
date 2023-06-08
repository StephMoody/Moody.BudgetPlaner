using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;
using Moody.BudgetPlaner.Avalonia.UI.Extensions;

namespace Moody.BudgetPlaner.Avalonia.UI;

public class Program
{
    public Task Run(IContainer container)
    {
        AppBuilder appBuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();

        appBuilder.StartWithClassicDesktopLifetime(Array.Empty<string>(), container); 
        return Task.CompletedTask;
    }
}