using Autofac;
using Moody.BudgetPlaner.Model.Autofac;
using Moody.BudgetPlaner.UI.Autofac;
using Moody.BudgetPlaner.UI.View;
using Moody.BudgetPlaner.UI.ViewModels;
using Moody.Common.Contracts;
using Moody.UI.Autofac;
using Moody.UI.Contracts;

namespace Moody.BudgetPlaner.Starter;

public class BudgetPlanerStarter
{
    private IContainer _container;

    public async Task Start()
    {
        ContainerBuilder containerBuilder = new();
        containerBuilder.RegisterModule(new UiModule());
        containerBuilder.RegisterModule(new BudgetPlanerUiModule());
        containerBuilder.RegisterModule(new BudgetPlanerModelModule());
        _container = containerBuilder.Build();

        IEnumerable<IInitializable> initializables = _container.Resolve<IEnumerable<IInitializable>>().ToList();
        await Task.WhenAll(initializables.Select(x => x.Initialize()));

        IWindowHost windowHost = _container.Resolve<IWindowHost>();
        await windowHost.ShowWindow(_container.Resolve<BudgetPlanerMainWindow>(), _container.Resolve<BudgetPlanerViewModel>());
    }

    public void Stop()
    {
        _container.Resolve<IDispatcherProvider>().Dispatcher.InvokeShutdown();
    }
}