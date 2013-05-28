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
            SudokuGrid newPuzzle = new SudokuGrid();



            return newPuzzle;
        }

        private void GenerateAnswer(SudokuGrid newPuzzle)
        {
            // Fill each row house with candidates 1 through 9, positioned randomly

            // Apply conflict minimization algorithm to rearrange grid to a final answer
        }

        #endregion
    }
}
