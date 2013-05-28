using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Enums;

namespace Sudoku.Model.Generator
{
    /// <summary>
    /// Class responsible for everything related to generating a new Sudoku puzzle.
    /// </summary>
    public class SudokuGenerator
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods

        /// <summary>
        /// Template algorithm for puzzle generation.
        /// </summary>
        /// <param name="diff"></param>
        /// <returns></returns>
        public SudokuGrid GenerateNewPuzzle(GameDifficultyEnum diff)
        {
            SudokuGrid newPuzzle = new SudokuGrid();

            return newPuzzle;
        }

        /// <summary>
        /// Fills each row of the Sudoku puzzle with digits 1 through 9, in a random order.
        /// </summary>
        /// <param name="newPuzzle"></param>
        private void GenerateAnswer(SudokuGrid newPuzzle)
        {

        }

        #endregion
    }
}
