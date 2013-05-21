using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Enums;

namespace Sudoku.Model.Generator
{
    public class SudokuGenerator
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods

        public SudokuGrid GenerateNewPuzzle(GameDifficultyEnum diff)
        {
            return new SudokuGrid();
        }

        #endregion
    }
}
