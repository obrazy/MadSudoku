using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Sudoku.Model.Grid;
using Sudoku.Model;

namespace Sudoku.ViewModel
{
    class SudokuGridViewModel : INotifyPropertyChanged
    {
        #region Properties

        // INotifyPropertyChanged related
        public event PropertyChangedEventHandler PropertyChanged;

        // SudokuGrid properties
        public TileViewModel[,] Tiles
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public SudokuGridViewModel()
        {
            Tiles = new TileViewModel[9, 9];
        }

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
