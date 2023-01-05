using Moody.PropertyChangedSourceGenerator;
using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

[ClassForGeneratePropertyChanged]
public partial class AddPositionViewModel : DialogViewModelBase
{ 
    [GeneratePropertyChanged]
    private DateTime _dueDate;
    private string _designation;
    private double _amount;

    public AddPositionViewModel(Action closeCommand) : base(closeCommand)
    {
        _amount = 0;
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