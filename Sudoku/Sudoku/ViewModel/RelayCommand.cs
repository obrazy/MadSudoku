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
        private Action<Object> _Action;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that takes the Action to perform for this command as parameter.
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action<Object> action)
        {
            this._Action = action;
        }

        #endregion

        #region Methods
        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            _Action(parameter);
        }

        #endregion
    }
}
