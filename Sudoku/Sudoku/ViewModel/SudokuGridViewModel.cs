using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model;

namespace Sudoku.ViewModel
{
    /// <summary>
    /// View-model class associated with the SudokuGrid model class.
    /// </summary>
    class SudokuGridViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Array of arrays of TileVieModel for the presentation of the puzzle's tiles.
        /// </summary>
        public TileViewModel[][] Tiles
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor that initiates the Tiles property.
        /// </summary>
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
        #endregion
    }
}
