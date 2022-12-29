

namespace Moody.BudgetPlaner.Model.Budget;

public class BudgetPosition : IBudgetPosition
{
    public BudgetPosition(string designation, double amount)
    {
        if (string.IsNullOrEmpty(designation))
            throw new InvalidOperationException("Missing designation!");
        
        Designation = designation;
        Amount = amount;
    }

    public string Designation { get; }

    public double Amount { get; }

}