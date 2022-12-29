namespace Moody.BudgetPlaner.Model.Budget;

public interface IBudgetPosition
{
    string Designation { get; }
    double Amount { get; }
}