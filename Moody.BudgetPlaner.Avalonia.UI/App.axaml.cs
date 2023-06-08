using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Moody.BudgetPlaner.Avalonia.UI.Extensions;
using Moody.BudgetPlaner.Avalonia.UI.View;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel;
using Moody.Common.Contracts;

namespace Moody.BudgetPlaner.Avalonia.UI;

public partial class App : Application
{

    private ILogger? _logger;
    private Window? _mainWindow;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is CustomClassicDesktopApplicationLifeTime desktop)
        {
            _logger = desktop.Container.Resolve<ILogger>();
            MainWindowViewModel mainWindowViewModel = desktop.Container.Resolve<MainWindowViewModel>();
            mainWindowViewModel.OnCloseRequested += MainWindowViewModelOnOnCloseRequested;
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel,
            };

            _mainWindow = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void MainWindowViewModelOnOnCloseRequested(object? sender, EventArgs e)
    {
        try
        {
            _mainWindow?.Close();
        }
        catch (Exception exception)
        {
            _logger?.Error(exception);
        }
    }
}