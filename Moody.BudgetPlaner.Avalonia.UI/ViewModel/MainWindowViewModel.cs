using System.Collections.ObjectModel;
using Moody.Avalonia.UI.ViewModel;

namespace Moody.BudgetPlaner.Avalonia.UI.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(AddBudgetPositionViewModel addBudgetPositionViewModel)
    {
        AddBudgetPositionViewModel = addBudgetPositionViewModel;
        AddBudgetPositionViewModel.PositionAdded += AddBudgetPositionViewModelOnPositionAdded;
    }

    public ObservableCollection<BudgetPositionViewModel> PositionViewModels { get; } = new()
    {
        new BudgetPositionViewModel(){Name = "Test", Amount = 200d}
    };

    public AddBudgetPositionViewModel AddBudgetPositionViewModel { get; }

    protected override void OnDispose()
    {
        AddBudgetPositionViewModel.PositionAdded -= AddBudgetPositionViewModelOnPositionAdded;
        base.OnDispose();
    }

    private void AddBudgetPositionViewModelOnPositionAdded(object? sender, AddBudgetPositionEventArgs e)
    {
        try
        {
            PositionViewModels.Add(e.AddedBudgetPositionViewModel);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}