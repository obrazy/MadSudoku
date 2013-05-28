using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model.Solver;
using Sudoku.Model.Generator;
using Sudoku.Enums;
using Sudoku.Model.Util;

namespace Sudoku.Model
{
    /// <summary>
    /// Class responsible to expose the model.
    /// </summary>
    public class ModelFacade
    {
        #region Properties

        /// <summary>
        /// Private instance of this class for the Singleton pattern.
        /// </summary>
        private static ModelFacade _Instance;

        /// <summary>
        /// Property for access to the Singleton's instance.
        /// </summary>
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

        /// <summary>
        /// The current Sudoku puzzle of the app.
        /// </summary>
        public SudokuGrid Puzzle { get; set; }

        /// <summary>
        /// Solver module.
        /// </summary>
        public SudokuSolver Solver;

        /// <summary>
        /// Generator module.
        /// </summary>
        public SudokuGenerator Generator;

        /// <summary>
        /// Utility module.
        /// </summary>
        public SudokuUtil Util;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor; instantiates the different modules of the model.
        /// </summary>
        private ModelFacade()
        {
            this.Puzzle = new SudokuGrid();
            this.Solver = new SudokuSolver();
            this.Generator = new SudokuGenerator();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a deep copy of the current Sudoku puzzle.
        /// </summary>
        /// <returns></returns>
        public SudokuGrid CloneGameGrid()
        {
            return new SudokuGrid(this.Puzzle);
        }

        /// <summary>
        /// Requests a new puzzle to be generated of the provided difficulty.
        /// </summary>
        /// <param name="diff"></param>
        public void RequestNewPuzzle(GameDifficultyEnum diff)
        {
            this.Puzzle = this.Generator.GenerateNewPuzzle(diff);
        }

        /// <summary>
        /// Returns a deep copy of the specified tile.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Tile CloneTile(int row, int col)
        {
            return this.Puzzle.Tiles[row][col];
        }

        #endregion
    }
}
