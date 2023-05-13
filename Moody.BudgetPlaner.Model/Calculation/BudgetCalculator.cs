using Moody.BudgetPlaner.Model.Budget;

namespace Moody.BudgetPlaner.Model.Calculation;

public class BudgetCalculator : IBudgetCalculator
{
    public  Task<double> Calculate(double monthlyIncome, IEnumerable<IBudgetPosition> positions, int? dueDayFilter)
    {
        int minimumDueDay = dueDayFilter ?? 0;
        
        double result = monthlyIncome - positions.Where(x=>x.DueDay >= minimumDueDay).Sum(x => x.Amount);
        return Task.FromResult(result);
    }
}