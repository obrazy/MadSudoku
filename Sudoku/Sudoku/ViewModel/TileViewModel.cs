using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sudoku.ViewModel
{
    class TileViewModel : INotifyPropertyChanged
    {

        #region Properties

        // INotifyPropertyChanged related
        public event PropertyChangedEventHandler PropertyChanged;

        // Tile properties
        public short CurrentValue { get; set; }
        public short Answer { get; set; }

        #endregion

        #region Constructors



        #endregion

        #region Methods

        // INotifyPropertyChanged related
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
