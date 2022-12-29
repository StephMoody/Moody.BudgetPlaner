namespace Moody.BudgetPlaner.Model.Budget;

public class BudgetPlan : IBudgetPlan
{
    private List<IBudgetPosition> _budgetPositions = new List<IBudgetPosition>();

    public IEnumerable<IBudgetPosition> BudgetPositions => _budgetPositions;

    public void AddPosition(IBudgetPosition position) => _budgetPositions.Add(position);

}