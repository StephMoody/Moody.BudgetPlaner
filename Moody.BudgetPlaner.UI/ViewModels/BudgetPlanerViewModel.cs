using System.Collections.ObjectModel;
using System.Windows.Input;
using Moody.Avalonia.UI.ViewModel;
using Moody.BudgetPlaner.Model.Budget;
using Moody.BudgetPlaner.Model.Calculation;
using Moody.BudgetPlaner.UI.View;
using Moody.Common.Tasks;
using Moody.UI.Command;
using Moody.UI.Contracts;
using Moody.UI.ViewModel;

namespace Moody.BudgetPlaner.UI.ViewModels;

public class BudgetPlanerViewModel : ViewModelBase
{
    private readonly Func<string, double, int,IBudgetPosition> _positionFactory;
    private readonly Func<IBudgetPosition, BudgetPositionViewModel> _budgetPositionViewModelFactory;
    private readonly Func<Action, AddPositionViewModel> _positionViewModelFactory;
    private readonly Func<AddPositionWindow> _positionWindowFactory;
    private readonly IBudgetCalculator _budgetCalculator;
    private readonly IWindowHost _windowHost;
    private double _balance;
    private int? _dueDayFilter;


    public BudgetPlanerViewModel(Func<string, double, int, IBudgetPosition> positionFactory,
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

    public int DueDayFilter
    {
        get => _dueDayFilter ?? 0;
        set
        {
            if (value == _dueDayFilter) return;
            _dueDayFilter = value;
            OnPropertyChanged();
        }
    }

    public IncomeViewModel IncomeViewModel { get; }

    private Task<bool> CanAddPosition(object? arg)
    {
        return Task.FromResult(true);
    }
    
    private async Task AddPosition(object? arg)
    {
        AddPositionWindow addPositionWindow = _positionWindowFactory.Invoke();
        AddPositionViewModel addPositionViewModel = _positionViewModelFactory.Invoke(() => addPositionWindow.Close());
        await _windowHost.ShowWindow(addPositionWindow, addPositionViewModel);
        
        BudgetPositionViewModel budgetPositionViewModel = _budgetPositionViewModelFactory.Invoke(_positionFactory.Invoke(addPositionViewModel.Designation, addPositionViewModel.Amount, addPositionViewModel.DueDay));
        budgetPositionViewModel.OnDeleteRequested += BudgetPositionViewModelOnOnDeleteRequested;
        BudgetPositionViewModels.Add(budgetPositionViewModel);
        await Recalculate();
    }

    private void BudgetPositionViewModelOnOnDeleteRequested(object? sender, EventArgs e)
    {
        try
        {
            if (sender is not BudgetPositionViewModel budgetPositionViewModel)
                throw new InvalidOperationException("");

            BudgetPositionViewModels.Remove(budgetPositionViewModel);
            Recalculate().FireAndForget();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private async Task Recalculate()
    {
        double calculate = await _budgetCalculator.Calculate(IncomeViewModel.MonthlyIncome,
            BudgetPositionViewModels.Select(x => x.BudgetPosition), _dueDayFilter);

        _balance = calculate;
        OnPropertyChanged(nameof(Balance));
    }
}