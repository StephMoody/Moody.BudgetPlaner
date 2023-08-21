using System.Windows.Input;
using Microsoft.Win32.SafeHandles;
using Moody.Common.Contracts;
using Moody.Common.UI.Command;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel;

public class BudgetPositionViewModel
{
    private readonly ILogger _logger;
    
    public BudgetPositionViewModel(ILogger logger)
    {
        _logger = logger;
        RemoveCommand = new RelayCommand(RemoveClicked);
    }

    public string? Name { get; set; }

    public double Amount { get; set; }

    public ICommand RemoveCommand { get; }

    private void RemoveClicked()
    {
        try
        {
            
        }
        catch (Exception e)
        {
            _logger.Error(e);
        }
    }
}