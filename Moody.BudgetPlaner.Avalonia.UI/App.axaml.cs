using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Moody.BudgetPlaner.Avalonia.UI.Extensions;
using Moody.BudgetPlaner.Avalonia.UI.View;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI;

public partial class App : Application
{
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is CustomClassicDesktopApplicationLifeTime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = desktop.Container.Resolve<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}