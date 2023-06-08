using System.Collections.ObjectModel;
using Moody.BudgetPlaner.Avalonia.UI.ViewModel.IncomeManagement;
using Moody.Common.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel.PositionManagement;

public class PositionManagementViewModel : ViewModelBase
{
    public PositionManagementViewModel(AddBudgetPositionViewModel addBudgetPositionViewModel, IncomeViewModel incomeViewModel)
    {
        AddBudgetPositionViewModel = addBudgetPositionViewModel;
    }
    
    public ObservableCollection<BudgetPositionViewModel> PositionViewModels { get; } = new()
    {
        new BudgetPositionViewModel(){Name = "Test", Amount = 200d},
        new BudgetPositionViewModel(){Name = "Test 2", Amount = 300d},
    };

    public AddBudgetPositionViewModel AddBudgetPositionViewModel { get; }
    
    private void AddBudgetPositionViewModelOnPositionAdded(object? sender, AddBudgetPositionEventArgs e)
    {
        try
        {
            PositionViewModels.Add(e.AddedBudgetPositionViewModel);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    protected override void OnDispose()
    {
        AddBudgetPositionViewModel.PositionAdded -= AddBudgetPositionViewModelOnPositionAdded;
        base.OnDispose();
    }
}