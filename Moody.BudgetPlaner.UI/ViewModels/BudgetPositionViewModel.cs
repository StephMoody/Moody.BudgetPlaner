using Moody.BudgetPlaner.Model.Budget;
using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

public class BudgetPositionViewModel : ViewModelBase
{
    private readonly IBudgetPosition _budgetPosition;

    public BudgetPositionViewModel(IBudgetPosition budgetPosition)
    {
        _budgetPosition = budgetPosition;
    }

    public string Designation => _budgetPosition.Designation;

    public double Amount => _budgetPosition.Amount;

    public IBudgetPosition BudgetPosition => _budgetPosition;
}