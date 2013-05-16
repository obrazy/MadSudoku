using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;

namespace Sudoku.Model
{
    public class ModelFacade
    {
        #region Properties

        // Singleton related properties

        private static ModelFacade _Instance;

        public static ModelFacade Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ModelFacade();
                }
                return _Instance;
            }
        }

        // Game related properties

        private SudokuGrid _GameGrid;

        #endregion

        #region Constructors

        private ModelFacade()
        {
        }

        #endregion

        #region Methods

        public SudokuGrid CloneGameGrid()
        {
            return new SudokuGrid(this._GameGrid);
        }

        #endregion
    }
}
