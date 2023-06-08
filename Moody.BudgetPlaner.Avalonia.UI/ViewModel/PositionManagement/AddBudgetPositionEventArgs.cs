namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel.PositionManagement;

public class AddBudgetPositionEventArgs : EventArgs
{
    public AddBudgetPositionEventArgs(BudgetPositionViewModel addedBudgetPositionViewModel)
    {
        AddedBudgetPositionViewModel = addedBudgetPositionViewModel;
    }

    public BudgetPositionViewModel AddedBudgetPositionViewModel { get; }
}