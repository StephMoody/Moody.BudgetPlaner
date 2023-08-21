using System.Collections.ObjectModel;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel.IncomeManagement;
using Moody.Common.Contracts;
using Moody.Common.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel.PositionManagement;

public class PositionManagementViewModel : ViewModelBase
{
    private readonly ILogger _logger;
    private readonly Func<BudgetPositionViewModel> _budgetPositionViewModelFactory;
    
    public PositionManagementViewModel(ILogger logger, 
        Func<BudgetPositionViewModel> budgetPositionViewModelFactory)
    {
        _logger = logger;
        _budgetPositionViewModelFactory = budgetPositionViewModelFactory;
    }

    public ObservableCollection<BudgetPositionViewModel> PositionViewModels { get; } = new();
}