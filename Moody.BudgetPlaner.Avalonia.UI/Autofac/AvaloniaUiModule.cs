using Autofac;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.Autofac;

public class AvaloniaUiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AddBudgetPositionViewModel>().AsSelf();
        builder.RegisterType<MainWindowViewModel>().AsSelf();
    }
}