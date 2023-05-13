using Autofac;
using Avalonia.Controls.ApplicationLifetimes;

namespace Moody.BudgetPlaner.Avalonia.UI.Extensions;

public class CustomClassicDesktopApplicationLifeTime : ClassicDesktopStyleApplicationLifetime
{
    public CustomClassicDesktopApplicationLifeTime(IContainer container)
    {
        Container = container;
    }

    internal IContainer Container { get; }

}