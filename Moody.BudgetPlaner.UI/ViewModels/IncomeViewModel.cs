using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

public class IncomeViewModel : ViewModelBase
{
    private double _monthlyIncome;

    public double MonthlyIncome
    {
        get => _monthlyIncome;
        set
        {
            if (value.Equals(_monthlyIncome)) return;
            _monthlyIncome = value;
            OnPropertyChanged();
        }
    }
}