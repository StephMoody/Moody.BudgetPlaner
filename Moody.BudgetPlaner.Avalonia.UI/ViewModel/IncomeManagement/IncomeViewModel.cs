using Moody.Common.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel.IncomeManagement;

public class IncomeViewModel : ViewModelBase
{
    private double _income;

    public double Income
    {
        get => _income;
        set
        {
            if (value.Equals(_income)) return;
            _income = value;
            OnPropertyChanged();
        }
    }
}