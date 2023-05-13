using Moody.PropertyChangedSourceGenerator;
using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

[ClassForGeneratePropertyChanged]
public partial class AddPositionViewModel : DialogViewModelBase
{ 
    private DateTime _dueDate;
    private string _designation;
    private double _amount;
    private int _dueDay;

    public AddPositionViewModel(Action closeCommand) : base(closeCommand)
    {
        _amount = 0;
        _dueDate = DateTime.Today;
        _designation = "Position";
    }
    
    public string Designation
    {
        get => _designation;
        set
        {
            if (value == _designation) 
                return;
            _designation = value;
            OnPropertyChanged();
        }
    }

    public int DueDay
    {
        get => _dueDay;
        set
        {
            if (value == _dueDay) return;
            _dueDay = value;
            OnPropertyChanged();
        }
    }

    public double Amount
    {
        get => _amount;
        set
        {
            if (value.Equals(_amount))
                return;
            _amount = value;
            OnPropertyChanged();
        }
    }
}