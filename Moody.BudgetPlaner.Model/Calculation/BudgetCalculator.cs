using Moody.BudgetPlaner.Model.Budget;

namespace Moody.BudgetPlaner.Model.Calculation;

public class BudgetCalculator : IBudgetCalculator
{
    public  Task<double> Calculate(double monthlyIncome, IEnumerable<IBudgetPosition> positions)
    {
        double result = monthlyIncome - positions.Sum(x => x.Amount);
        return Task.FromResult(result);
    }
}