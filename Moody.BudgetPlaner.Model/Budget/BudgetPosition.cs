

namespace Moody.BudgetPlaner.Model.Budget;

public class BudgetPosition : IBudgetPosition
{
    public BudgetPosition(string designation, double amount, int dueDay)
    {
        if (string.IsNullOrEmpty(designation))
            throw new InvalidOperationException("Missing designation!");
        
        Designation = designation;
        Amount = amount;
        DueDay = dueDay;
    }

    public string Designation { get; }

    public double Amount { get; }
    public int DueDay { get; }
}