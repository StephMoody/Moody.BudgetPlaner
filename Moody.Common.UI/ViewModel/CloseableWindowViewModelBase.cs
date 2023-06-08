using System.Windows.Input;
using Moody.Common.Contracts;
using Moody.Common.UI.Command;

namespace Moody.Common.UI.ViewModel;

public abstract class CloseableWindowViewModelBase : ViewModelBase
{
    private readonly ILogger _logger;

    protected CloseableWindowViewModelBase(ILogger logger)
    {
        _logger = logger;
        CloseCommand = new RelayCommand(ExecuteClose);
    }

    public ICommand CloseCommand { get; }

    private void ExecuteClose()
    {
        try
        {
            OnCloseRequested.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            _logger.Error(e);
        }
    }

    public event EventHandler OnCloseRequested;
}