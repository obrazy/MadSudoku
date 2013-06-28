using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Enums;
using Sudoku.Model.Util;

namespace Sudoku.Model.Generator
{
    /// <summary>
    /// Class responsible for everything related to generating a new Sudoku puzzle.
    /// </summary>
    public class SudokuGenerator
    {
        #region Properties

        /// <summary>
        /// The random number generator used to generate the puzzle.
        /// </summary>
        private Random _rng { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor.
        /// </summary>
        public SudokuGenerator()
        {
            this._rng = new Random();
        }

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

            FillOneToNine(newPuzzle);

            return newPuzzle;
        }

        /// <summary>
        /// Fills each row of the Sudoku puzzle with digits 1 through 9, in a random order.
        /// </summary>
        /// <param name="newPuzzle"></param>
        private void FillOneToNine(SudokuGrid newPuzzle)
        {
            for (int i = 0; i < 9; ++i)
            {
                IList<Cell> curRow = SudokuUtil.GetRowHouse(i, newPuzzle);

                IList<int> digits = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int j = 0; j < 9; ++j)
                {
                    int nextDigit = this._rng.Next(digits.Count);
                    curRow[j].Answer = digits[nextDigit];
                    digits.RemoveAt(nextDigit);
                }
            }
        }

        /// <summary>
        /// Minimizes conflicts in the grid passed as argument, resulting in a grid that is a
        /// legal Sudoku solution.
        /// </summary>
        /// <param name="newPuzzle"></param>
        private void MinConflicts(SudokuGrid newPuzzle)
        {
            // Evaluate initial number of conflicts for each cell
            // Repeat while conflicts remain:
            //  Select a random cell that has remaining conflicts
            //  For each cell of the same row as the selected cell:
            //      Simulate a swap between both cells
            //      Calculate number of conflicts for those two cells after the swap
            //      If the number of conflicts has diminished, save that cell and the total number of conflicts of those two cells
            //  Select the swap that results in the least amount of conflicts and perform it (for ties, select randomly)
        }

        #endregion
    }
}