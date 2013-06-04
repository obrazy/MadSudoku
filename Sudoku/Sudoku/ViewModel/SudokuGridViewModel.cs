using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model;
using System.Windows.Input;
using Sudoku.Enums;
using System.Collections.ObjectModel;
using Sudoku.Model.Util;

namespace Sudoku.ViewModel
{
    //http://channel9.msdn.com/coding4fun/articles/Building-a-WPF-Sudoku-Game-Part-2-The-Board-UI-and-Validation

    /// <summary>
    /// View-model class associated with the SudokuGrid model class.
    /// </summary>
    public class SudokuGridViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// The list of rows which are in turn lists of TileViewModel.
        /// </summary>
        private ObservableCollection<ObservableCollection<TileViewModel>> _rows;

        /// <summary>
        /// The list of the square houses, which are lists of TileViewModel.
        /// </summary>
        private ObservableCollection<ObservableCollection<TileViewModel>> _squareHouses;
        public ObservableCollection<ObservableCollection<TileViewModel>> SquareHouses
        {
            get
            {
                if (this._squareHouses != null)
                {
                    return this._squareHouses;
                }
                else
                {
                    this._squareHouses = new ObservableCollection<ObservableCollection<TileViewModel>>();
                    this.ConvertRowsToSquares();
                    return this._squareHouses;
                }
            }
        }        

        /// <summary>
        /// Command to reset the puzzle.
        /// </summary>
        public ICommand NewPuzzleCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor.
        /// </summary>
        public SudokuGridViewModel()
        {
            this._rows = new ObservableCollection<ObservableCollection<TileViewModel>>();

            // Build the rows of the puzzle
            for (int i = 0; i < 9; ++i)
            {
                ObservableCollection<TileViewModel> row = new ObservableCollection<TileViewModel>();
                for (int j = 0; j < 9; ++j)
                {
                    TileViewModel tvm = new TileViewModel(i, j);
                    row.Add(tvm);
                }

                this._rows.Add(row);
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
            // TEMP Hardcoded to request a new Easy puzzle
            ModelFacade.Instance.RequestNewPuzzle(GameDifficultyEnum.EASY);
            this.RefreshPuzzle();
            this._squareHouses = null;
            this.NotifyPropertyChanged("SquareHouses");
        }

        /// <summary>
        /// Rebuilds the Tiles property to synchronize the view-model with the model.
        /// </summary>
        private void RefreshPuzzle()
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    this._rows[i][j] = new TileViewModel(i, j);
                }
            }
        }

        /// <summary>
        /// Using the _rows property, populate the _squareHouses property to use lists of square houses
        /// instead of lists of rows.
        /// </summary>
        private void ConvertRowsToSquares()
        {
            for (int i = 1; i < 10; ++i)
            {
                this._squareHouses.Add(new ObservableCollection<TileViewModel>(SudokuUtil.GetSquareHouse(i, _rows)));
            }
        }

        #endregion
    }
}
