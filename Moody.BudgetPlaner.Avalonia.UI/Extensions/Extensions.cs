using Autofac;
using Avalonia.Controls;

namespace Moody.BudgetPlaner.Avalonia.UI.Extensions;

public static class Extensions
{
    public static int StartWithClassicDesktopLifetime<T>(
        this T builder, string[] args, IContainer container, ShutdownMode shutdownMode = ShutdownMode.OnLastWindowClose)
        where T : AppBuilderBase<T>, new()
    {
        CustomClassicDesktopApplicationLifeTime lifetime = new(container)
        {
            Args = args,
            ShutdownMode = shutdownMode
        };
        builder.SetupWithLifetime(lifetime);
        return lifetime.Start(args);
    }
}