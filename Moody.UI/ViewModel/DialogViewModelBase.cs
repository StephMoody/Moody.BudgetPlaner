using System;
using System.Windows.Input;
using Moody.UI.Command;

namespace Moody.UI.ViewModel;

public abstract class DialogViewModelBase : ViewModelBase
{
    private readonly Action _closeCommand;

    public DialogViewModelBase(Action closeCommand)
    {
        _closeCommand = closeCommand;
        AcceptCommand = new RelayCommand(OnAccept);
    }

    public ICommand AcceptCommand { get; }

    protected virtual void OnAccept()
    {
        _closeCommand.Invoke();
    }
        
}