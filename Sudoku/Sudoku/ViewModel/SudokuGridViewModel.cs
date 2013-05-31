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

        private ObservableCollection<ObservableCollection<TileViewModel>> _rows;
        public ObservableCollection<ObservableCollection<TileViewModel>> Rows
        {
            get
            {
                return this._rows;
            }
        }

        public TileViewModel[][] Tiles { get; set; }

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
            }

            //this.Tiles = new TileViewModel[9][];

            //for (int i = 0; i < 9; ++i)
            //{
            //    this.Tiles[i] = new TileViewModel[9];
            //}

            //this.RefreshPuzzle();

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
            //this.RefreshPuzzle();
            //this.NotifyPropertyChanged("Tiles");
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
                    this.Tiles[i][j] = new TileViewModel(i, j);
                }
            }
        }

        #endregion
    }
}
