using System.Collections.ObjectModel;
using System.Windows.Input;
using Moody.BudgetPlaner.Model.Budget;
using Moody.BudgetPlaner.Model.Calculation;
using Moody.BudgetPlaner.UI.View;
using Moody.UI.Command;
using Moody.UI.Contracts;
using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

public class BudgetPlanerViewModel : ViewModelBase
{
    private readonly Func<string, double, IBudgetPosition> _positionFactory;
    private readonly Func<IBudgetPosition, BudgetPositionViewModel> _budgetPositionViewModelFactory;
    private readonly Func<Action, AddPositionViewModel> _positionViewModelFactory;
    private readonly Func<AddPositionWindow> _positionWindowFactory;
    private readonly IBudgetCalculator _budgetCalculator;
    private readonly IWindowHost _windowHost;
    private double _balance;


    public BudgetPlanerViewModel(Func<string, double, IBudgetPosition> positionFactory,
        Func<Func<object?, Task>, Func<object?, Task<bool>>, AsyncCommand> asyncCommandFactory,
        Func<IBudgetPosition, BudgetPositionViewModel> budgetPositionViewModelFactory,
        IBudgetCalculator budgetCalculator,
        IncomeViewModel incomeViewModel,
        IWindowHost windowHost,
        Func<Action, AddPositionViewModel> positionViewModelFactory,
        Func<AddPositionWindow> positionWindowFactory)
    {
        _positionFactory = positionFactory;
        _budgetPositionViewModelFactory = budgetPositionViewModelFactory;
        _budgetCalculator = budgetCalculator;
        IncomeViewModel = incomeViewModel;
        _windowHost = windowHost;
        _positionViewModelFactory = positionViewModelFactory;
        _positionWindowFactory = positionWindowFactory;
        AddCommand = asyncCommandFactory.Invoke(AddPosition,
            CanAddPosition);

    }

    public ObservableCollection<BudgetPositionViewModel> BudgetPositionViewModels { get; } = new();

    public ICommand AddCommand { get; }

    
    public double Balance => _balance;

    public IncomeViewModel IncomeViewModel { get; }

    private async Task<bool> CanAddPosition(object? arg)
    {
        await Task.Delay(5000);
        return true;
    }
    
    private async Task AddPosition(object? arg)
    {
        AddPositionWindow addPositionWindow = _positionWindowFactory.Invoke();
        AddPositionViewModel addPositionViewModel = _positionViewModelFactory.Invoke(() => addPositionWindow.Close());
        await _windowHost.ShowWindow(addPositionWindow, addPositionViewModel);
        
        BudgetPositionViewModel budgetPositionViewModel = _budgetPositionViewModelFactory.Invoke(_positionFactory.Invoke(addPositionViewModel.Designation, addPositionViewModel.Amount));
        BudgetPositionViewModels.Add(budgetPositionViewModel);
        await Recalculate();
    }

    private async Task Recalculate()
    {
        double calculate = await _budgetCalculator.Calculate(IncomeViewModel.MonthlyIncome,
            BudgetPositionViewModels.Select(x => x.BudgetPosition));

        _balance = calculate;
        OnPropertyChanged(nameof(Balance));
    }
}