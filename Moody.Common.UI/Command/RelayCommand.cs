﻿using System.Windows.Input;

namespace Moody.Common.UI.Command;

public class RelayCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly Action _action;

    public RelayCommand(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action();
    }
}