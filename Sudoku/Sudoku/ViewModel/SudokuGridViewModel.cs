using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model;
using System.Windows.Input;
using Sudoku.Enums;
using System.Collections.ObjectModel;

namespace Sudoku.ViewModel
{
    //http://channel9.msdn.com/coding4fun/articles/Building-a-WPF-Sudoku-Game-Part-2-The-Board-UI-and-Validation

    /// <summary>
    /// View-model class associated with the SudokuGrid model class.
    /// </summary>
    class SudokuGridViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// The list of rows which are in turn lists of TileViewModel.
        /// </summary>
        private ObservableCollection<ObservableCollection<TileViewModel>> _rows;
        public ObservableCollection<ObservableCollection<TileViewModel>> Rows
        {
            get
            {
                return this._rows;
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
            _rows = new ObservableCollection<ObservableCollection<TileViewModel>>();
            for (int i = 0; i < 9; ++i)
            {
                ObservableCollection<TileViewModel> row = new ObservableCollection<TileViewModel>();
                for (int j = 0; j < 9; ++j)
                {
                    TileViewModel tvm = new TileViewModel(i, j);
                    row.Add(tvm);
                }

                _rows.Add(row);
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
                    this.Rows[i][j] = new TileViewModel(i, j);
                }
            }
        }

        #endregion
    }
}
