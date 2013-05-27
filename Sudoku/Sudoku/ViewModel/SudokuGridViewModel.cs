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
        public TileViewModel[][] Tiles
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public SudokuGridViewModel()
        {
            this.Tiles = new TileViewModel[9][];

            for (Int16 i = 0; i < 9; ++i)
            {
                this.Tiles[i] = new TileViewModel[9];
            }

            for (Int16 i = 0; i < 9; ++i)
            {
                for (Int16 j = 0; j < 9; ++j)
                {
                    this.Tiles[i][j] = new TileViewModel(i, j);
                }
            }
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
