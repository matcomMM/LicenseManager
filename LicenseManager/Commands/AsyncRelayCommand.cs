using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Commands
{
    public class AsyncRelayCommand<T> : AsyncCommandBase
    {
        private readonly Func<Task> _callback;
        private readonly Predicate<T> _canExecute;

        public AsyncRelayCommand(Func<Task> callbacke, Action<Exception> onException) : this(callbacke, null, onException) { }

        public AsyncRelayCommand(Func<Task> callback, Predicate<T> canExecute, Action<Exception> onException) : base(onException)
        {
            _callback = callback;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return (_canExecute == null || _canExecute((T)parameter)) && base.CanExecute(parameter);
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }
}