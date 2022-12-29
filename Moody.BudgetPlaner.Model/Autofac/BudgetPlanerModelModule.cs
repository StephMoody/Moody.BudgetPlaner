using Autofac;
using Moody.BudgetPlaner.Model.Budget;
using Moody.BudgetPlaner.Model.Calculation;

namespace Moody.BudgetPlaner.Model.Autofac;

public class BudgetPlanerModelModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BudgetPlan>().As<IBudgetPlan>();
        builder.RegisterType<BudgetPosition>().As<IBudgetPosition>();
        builder.RegisterType<BudgetCalculator>().As<IBudgetCalculator>();
        base.Load(builder);
    }
}