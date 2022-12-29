using Autofac;
using Moody.BudgetPlaner.UI.View;
using Moody.BudgetPlaner.UI.ViewModels;

namespace Moody.BudgetPlaner.UI.Autofac;

public class BudgetPlanerUiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BudgetPlanerViewModel>().AsSelf().SingleInstance();
        builder.RegisterType<BudgetPlanerMainWindow>().AsSelf().SingleInstance();
        builder.RegisterType<IncomeViewModel>().AsSelf().SingleInstance();
        builder.RegisterType<BudgetPositionViewModel>().AsSelf().InstancePerDependency();
        builder.RegisterType<AddPositionWindow>().AsSelf().InstancePerDependency();
        builder.RegisterType<AddPositionViewModel>().AsSelf().InstancePerDependency();
        base.Load(builder);
    }
}