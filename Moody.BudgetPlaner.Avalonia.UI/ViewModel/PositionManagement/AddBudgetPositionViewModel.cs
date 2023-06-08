using System.Windows.Input;
using Moody.Common.UI.Command;
using Moody.Common.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel.PositionManagement;

public class AddBudgetPositionViewModel : ViewModelBase
{
    private string? _name;
    private double _amount;

    public AddBudgetPositionViewModel()
    {
        AddPositionCommand = new RelayCommand(ExecuteAddPositionCommand);
    }

    public event EventHandler<AddBudgetPositionEventArgs>? PositionAdded;

    public ICommand AddPositionCommand { get; }

    public string? Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public double Amount
    {
        get => _amount;
        set
        {
            if (value.Equals(_amount)) return;
            _amount = value;
            OnPropertyChanged();
        }
    }

    private void ExecuteAddPositionCommand()
    {
        BudgetPositionViewModel budgetPositionViewModel = new BudgetPositionViewModel
        {
            Name = _name, Amount = _amount
        };

        PositionAdded?.Invoke(this, new AddBudgetPositionEventArgs(budgetPositionViewModel));
    }
}