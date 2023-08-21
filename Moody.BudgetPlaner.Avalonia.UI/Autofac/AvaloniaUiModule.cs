using Autofac;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel.IncomeManagement;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel.PositionManagement;

namespace Moody.BudgetPlaner.Avalonia.UI.Autofac;

public class AvaloniaUiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PositionManagementViewModel>().AsSelf();
        builder.RegisterType<BudgetPositionViewModel>().InstancePerDependency();
        builder.RegisterType<IncomeViewModel>().AsSelf();
        builder.RegisterType<MainWindowViewModel>().AsSelf();
    }
    
}