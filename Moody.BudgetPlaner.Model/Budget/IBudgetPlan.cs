namespace Moody.BudgetPlaner.Model.Budget;

public interface IBudgetPlan
{
    IEnumerable<IBudgetPosition> BudgetPositions { get; }
    void AddPosition(IBudgetPosition position);
}