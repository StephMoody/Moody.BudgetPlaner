using Moody.Avalonia.UI.ViewModel;
using Moody.BudgetPlaner.Model.Budget;
using Moody.UI.Command;
using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

public class BudgetPositionViewModel : ViewModelBase
{
    private readonly IBudgetPosition _budgetPosition;

    public BudgetPositionViewModel(IBudgetPosition budgetPosition, IAsyncCommandFactory asyncCommandFactory)
    {
        _budgetPosition = budgetPosition;
        RequestDeleteCommand = asyncCommandFactory.Create(ExecuteRequestDelete);
    }
    
    public AsyncCommand RequestDeleteCommand { get; }
    
    public string Designation => _budgetPosition.Designation;

    public double Amount => _budgetPosition.Amount;

    public event EventHandler? OnDeleteRequested;
         
    public IBudgetPosition BudgetPosition => _budgetPosition;

    private Task ExecuteRequestDelete(object? arg)
    {
        OnDeleteRequested?.Invoke(this, EventArgs.Empty);
        return Task.CompletedTask;
    }
}