using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Blog.Client
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> canExecutePredicate;
        private readonly Action<object> executeAction;

        /// <summary>
        /// Creates a new command that can always executeAction.
        /// </summary>
        /// <param name="executeAction">The execution logic.</param>
        public RelayCommand(Action<object> executeAction)
            : this(executeAction, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            executeAction = execute;
            canExecutePredicate = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return canExecutePredicate == null || canExecutePredicate(parameters);
        }

        public void Execute(object parameters)
        {
            executeAction(parameters);
        }
    }
}
