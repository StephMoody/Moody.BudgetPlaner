using System;
using System.Threading.Tasks;

namespace Moody.UI.Command;

public interface IAsyncCommandFactory
{
    AsyncCommand Create(Func<object?, Task> executeAsync);
    AsyncCommand Create(Func<object?, Task> executeAsync, Func<object?, Task<bool>> canExecuteAsync);
}