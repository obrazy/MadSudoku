using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model.Solver;
using Sudoku.Model.Generator;
using Sudoku.Enums;

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

        public SudokuGrid Puzzle { get; set; }
        public SudokuSolver Solver;
        public SudokuGenerator Generator;

        #endregion

        #region Constructors

        private ModelFacade()
        {
            this.Puzzle = new SudokuGrid();
            this.Solver = new SudokuSolver();
            this.Generator = new SudokuGenerator();
        }

        #endregion

        #region Methods

        public SudokuGrid CloneGameGrid()
        {
            return new SudokuGrid(this.Puzzle);
        }

        public void RequestNewPuzzle(GameDifficultyEnum diff)
        {
            this.Puzzle = this.Generator.GenerateNewPuzzle(diff);
        }

        public Tile CloneTile(Int16 row, Int16 col)
        {
            return this.Puzzle.Tiles[row][col];
        }

        #endregion
    }
}
