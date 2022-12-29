using System.Windows.Threading;
using Moody.Common.Contracts;

namespace Moody.UI.Contracts;

public interface IDispatcherProvider : IInitializable
{
    Dispatcher Dispatcher { get; }
}