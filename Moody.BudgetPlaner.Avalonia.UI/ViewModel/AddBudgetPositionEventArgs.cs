namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel;

public class AddBudgetPositionEventArgs : EventArgs
{
    public AddBudgetPositionEventArgs(BudgetPositionViewModel addedBudgetPositionViewModel)
    {
        AddedBudgetPositionViewModel = addedBudgetPositionViewModel;
    }

    public BudgetPositionViewModel AddedBudgetPositionViewModel { get; }
}