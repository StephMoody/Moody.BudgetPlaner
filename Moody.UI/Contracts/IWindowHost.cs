using System.Threading.Tasks;
using System.Windows;

namespace Moody.UI.Contracts;

public interface IWindowHost
{
    Task ShowWindow(object viewModel);

    Task ShowWindow(Window window, object viewModel);
}