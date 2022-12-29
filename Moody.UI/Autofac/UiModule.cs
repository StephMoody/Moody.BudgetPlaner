using Autofac;
using Moody.Common.Contracts;
using Moody.UI.Command;
using Moody.UI.Contracts;
using Moody.UI.Model;

namespace Moody.UI.Autofac;

public class UiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DispatcherProvider>().As<IDispatcherProvider>().As<IInitializable>().SingleInstance();
        builder.RegisterType<WindowHost>().As<IWindowHost>().SingleInstance();
        builder.RegisterType<AsyncCommand>().AsSelf().InstancePerDependency();
        base.Load(builder);
    }
}