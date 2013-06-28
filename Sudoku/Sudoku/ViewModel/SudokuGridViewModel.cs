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
    /// <summary>
    /// View-model class associated with the SudokuGrid model class.
    /// </summary>
    public class SudokuGridViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// The list of rows which are in turn lists of CellViewModel.
        /// </summary>
        private ObservableCollection<ObservableCollection<CellViewModel>> _rows;

        /// <summary>
        /// The list of the square houses, which are lists of CellViewModel.
        /// </summary>
        private ObservableCollection<ObservableCollection<CellViewModel>> _squareHouses;
        public ObservableCollection<ObservableCollection<CellViewModel>> SquareHouses
        {
            get
            {
                if (this._squareHouses != null)
                {
                    return this._squareHouses;
                }
                else
                {
                    this._squareHouses = new ObservableCollection<ObservableCollection<CellViewModel>>();
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
            this._rows = new ObservableCollection<ObservableCollection<CellViewModel>>();

            // Build the rows of the puzzle
            for (int i = 0; i < 9; ++i)
            {
                ObservableCollection<CellViewModel> row = new ObservableCollection<CellViewModel>();
                for (int j = 0; j < 9; ++j)
                {
                    CellViewModel tvm = new CellViewModel(i, j);
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
        /// Rebuilds the Cells property to synchronize the view-model with the model.
        /// </summary>
        private void RefreshPuzzle()
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    this._rows[i][j] = new CellViewModel(i, j);
                }
            }
        }

        /// <summary>
        /// Using the _rows property, populate the _squareHouses property to use lists of square houses
        /// instead of lists of rows.
        /// </summary>
        private void ConvertRowsToSquares()
        {
            for (int i = 0; i < 9; ++i)
            {
                this._squareHouses.Add(new ObservableCollection<CellViewModel>(SudokuUtil.GetSquareHouse(i, _rows)));
            }
        }

        #endregion
    }
}