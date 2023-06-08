using System.Collections.ObjectModel;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel.IncomeManagement;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel.PositionManagement;
using Moody.Common.Contracts;
using Moody.Common.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel;

public class MainWindowViewModel : CloseableWindowViewModelBase
{
    public MainWindowViewModel(ILogger logger, PositionManagementViewModel positionManagementViewModel, IncomeViewModel incomeViewModel) : base(logger)
    {
        PositionManagementViewModel = positionManagementViewModel;
        IncomeViewModel = incomeViewModel;
    }

    public PositionManagementViewModel PositionManagementViewModel { get; }

    public IncomeViewModel IncomeViewModel { get; }

    protected override void OnDispose()
    {
        PositionManagementViewModel.Dispose();
        base.OnDispose();
    }
}