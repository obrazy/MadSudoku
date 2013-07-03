using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Enums;
using Sudoku.Model.Util;
using System.Collections;

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

        /// <summary>
        /// The data structure to keep track of which cells have how many conflicts. Because there will
        /// always be 9 rows and 9 columns in the puzzle, the key used for a cell is the concatenation
        /// of its row number and its column number, the concatenation result being used as an int.
        /// </summary>
        private Hashtable _conflicts;

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor.
        /// </summary>
        public SudokuGenerator()
        {
            this._rng = new Random();
            this._conflicts = new Hashtable();
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
            InitializeConflicts(newPuzzle);
            MinConflicts(newPuzzle);

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
        /// Calculates initial conflicts for every cell. Keeps track only of cells
        /// that have at least 1 conflict.
        /// </summary>
        /// <param name="newPuzzle"></param>
        private void InitializeConflicts(SudokuGrid newPuzzle)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    int answer = newPuzzle.Cells[i][j].Answer;
                    int key = 10 * i + j;
                    int nConflicts = SudokuUtil.GetVisibleCellsModified(i, j, newPuzzle).Count(c => c.Answer == answer) - 1;
                    if (nConflicts > 0)
                    {
                        this._conflicts[key] = nConflicts;
                    }
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
            // Repeat while conflicts remain:
            //  Select a random cell that has remaining conflicts
            //  For each cell of the same row as the selected cell:
            //      Simulate a swap between both cells
            //      Calculate differential in overall number of conflicts
            //      If differential is negative or zero, remember that swap and the number of conflicts
            //  Select the swap that has the smallest differential and perform it (for ties, select randomly)
            while (this._conflicts.Count > 0)
            {
                //int currentCellKey = this._conflicts.Keys.to[this._rng.Next(this._conflicts.Count)];
            }
        }

        #endregion
    }
}