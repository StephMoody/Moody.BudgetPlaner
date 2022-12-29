using Moody.BudgetPlaner.Model.Budget;

namespace Moody.BudgetPlaner.Model.Calculation;

public interface IBudgetCalculator
{
    Task<double> Calculate(double monthlyIncome, IEnumerable<IBudgetPosition> positions);
}