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
        CloseCommand = new RelayCommand(OnClose);
    }

    public ICommand AcceptCommand { get; }

    public ICommand CloseCommand { get; }

    protected virtual void OnAccept()
    {
        _closeCommand.Invoke();
    }
    
    protected virtual void OnClose()
    {
        _closeCommand.Invoke();
    }
}