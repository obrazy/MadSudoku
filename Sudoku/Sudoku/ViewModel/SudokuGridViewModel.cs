using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model;
using System.Windows.Input;
using Sudoku.Enums;

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

        /// <summary>
        /// Command to reset the puzzle.
        /// </summary>
        public ICommand NewPuzzleCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor that initiates the properties and the commands.
        /// </summary>
        public SudokuGridViewModel()
        {
            this.Tiles = new TileViewModel[9][];

            for (int i = 0; i < 9; ++i)
            {
                this.Tiles[i] = new TileViewModel[9];
            }

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    this.Tiles[i][j] = new TileViewModel(i, j);
                }
            }

            // Commands
            this.NewPuzzleCommand = new RelayCommand(new Action<object>(this.RequestNewPuzzle));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Action to perform when requesting a new puzzle.
        /// </summary>
        /// <param name="o"></param>
        private void RequestNewPuzzle(object o)
        {
            ModelFacade.Instance.RequestNewPuzzle(GameDifficultyEnum.EASY);
            this.NotifyPropertyChanged("Tiles");
        }

        #endregion
    }
}
