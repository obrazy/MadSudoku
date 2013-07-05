using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Sudoku.ViewModel
{
    /// <summary>
    /// Class representing a command from the view.
    /// </summary>
    class RelayCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// The Action to perform for this command.
        /// </summary>
        private Action<Object> _action;

        /// <summary>
        /// Predicated used to determine whether the action of this command can be executed or not.
        /// </summary>
        private Predicate<object> _canExecutePredicate;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that takes the Action to perform for this command as parameter.
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action<Object> action, Predicate<object> canExecPredicate)
        {
            this._action = action;
            this._canExecutePredicate = canExecPredicate;
        }

        #endregion

        #region Methods
        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(Object parameter)
        {
            return this._canExecutePredicate == null ? true : this._canExecutePredicate.Invoke(parameter);
        }

        public void Execute(Object parameter)
        {
            _action(parameter);
        }

        #endregion
    }
}
