using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sudoku.ViewModel
{
    /// <summary>
    /// Base class for the view-model classes to derive from.
    /// </summary>
    class ViewModelBase : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Event handler for the INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors
        #endregion

        #region Methods

        /// <summary>
        /// Notifies that a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        #endregion
    }
}
