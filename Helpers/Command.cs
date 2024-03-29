﻿using System;
using System.Windows.Input;

namespace TheWeather.Helpers
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public Command(Action execute, Func<object, bool> canExecute = null)
        {
            _execute = new Action<object>(_ => execute());
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
