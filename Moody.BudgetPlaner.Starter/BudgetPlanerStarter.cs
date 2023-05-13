using Autofac;
using Moody.BudgetPlaner.Avalonia.UI.Autofac;
using Moody.BudgetPlaner.Model.Autofac;
using Moody.Common.Contracts;

namespace Moody.BudgetPlaner.Starter;

public class BudgetPlanerStarter
{

    public async Task Start()
    {
        ContainerBuilder containerBuilder = new();
        containerBuilder.RegisterModule(new BudgetPlanerModelModule());
        containerBuilder.RegisterModule(new AvaloniaUiModule());

        IContainer container = containerBuilder.Build();

        IEnumerable<IInitializable> initializables = container.Resolve<IEnumerable<IInitializable>>().ToList();
        await Task.WhenAll(initializables.Select(x => x.Initialize()));
        await new Moody.BudgetPlaner.Avalonia.UI.Program().Run(container);
    }
}