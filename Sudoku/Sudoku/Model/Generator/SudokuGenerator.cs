#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Enums;
using Sudoku.Model.Util;
using System.Collections;
using System.Diagnostics;

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
        /// The data structure to keep track of which cells have how many conflicts.
        /// </summary>
        private SudokuCellContainer _conflicts;

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor.
        /// </summary>
        public SudokuGenerator()
        {
            this._rng = new Random();
            this._conflicts = new SudokuCellContainer();
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
                    Cell currCell = newPuzzle.Cells[i][j];
                    currCell.NumberOfConflicts = SudokuUtil.GetVisibleCellsModified(i, j, newPuzzle).Count(c => c.Answer == currCell.Answer);
                    if (currCell.NumberOfConflicts > 0)
                    {
                        this._conflicts.Add(currCell);
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
            #if DEBUG
            StringBuilder debugMessage;
            #endif

            int nIterations = 0;
            while (!this._conflicts.IsEmpty())
            {
                Cell currCell = this._conflicts.GetRandomCell();

                #if DEBUG
                    debugMessage = new StringBuilder();
                    debugMessage.Append("\n=========================================\n\n");
                    debugMessage.Append("Iteration: " + nIterations + "\n\n");
                    debugMessage.Append("Initial Grid:\n\n" + newPuzzle);
                    debugMessage.Append("\n\nInitial Current Cell:\n\tPosition: (" + currCell.Row + ", " + currCell.Col +
                                        ")\n\tValue: " + currCell.Answer + "\n\tConflicts: " + currCell.NumberOfConflicts + "\n\n");
                #endif

                IList<Tuple<int, int>> scores = new List<Tuple<int, int>>();    // <swapPosition, numberOfConflicts>

                IList<Cell> currCellVisibleCells = SudokuUtil.GetVisibleCellsModified(currCell.Row, currCell.Col, newPuzzle);
                int currCellInitialConflicts = currCell.NumberOfConflicts;

                foreach (Cell c in SudokuUtil.GetRowHouse(currCell.Row, newPuzzle))
                {
                    if (c.Row == currCell.Row && c.Col == currCell.Col)
                    {
                        continue;
                    }

                    IList<Cell> newCellVisibleCells = SudokuUtil.GetVisibleCellsModified(c.Row, c.Col, newPuzzle);
                    int newCellInitialConflicts = c.NumberOfConflicts;
                    int currCellFinalConflicts = newCellVisibleCells.Count(x => x.Answer == currCell.Answer);
                    int newCellFinalConflicts = currCellVisibleCells.Count(x => x.Answer == c.Answer);

                    if ((currCellFinalConflicts + newCellFinalConflicts) < (currCellInitialConflicts + newCellInitialConflicts))    // Only consider the swap if the resulting total number of conflicts is strictly decreased
                    {
                        if (scores.Count == 0 || (currCellFinalConflicts + newCellFinalConflicts) <= scores[0].Item2)
                        {
                            scores.Clear();
                            scores.Add(Tuple.Create(c.Col, currCellFinalConflicts + newCellFinalConflicts));
                        }
                    }
                }

                if (scores.Count > 0)
                {
                    int swapIndex = scores[this._rng.Next(scores.Count)].Item1;
                    Cell swapDestinationCell = newPuzzle.Cells[currCell.Row][swapIndex];
                    int currCellNewConflicts = 0;
                    int swapCellNewConflicts = 0;

#if DEBUG
                    debugMessage.Append("Initial Swap Cell:\n\tPosition: (" + swapDestinationCell.Row + ", " + swapDestinationCell.Col +
                                        ")\n\tValue: " + swapDestinationCell.Answer + "\n\tConflicts: " + swapDestinationCell.NumberOfConflicts + "\n\n\n");
                    StringBuilder decrementedConflicts = new StringBuilder();
                    StringBuilder incrementedConflicts = new StringBuilder();
#endif

                    foreach (Cell c in currCellVisibleCells)
                    {
                        if (c.Answer == currCell.Answer)
                        {
                            --c.NumberOfConflicts;

#if DEBUG
                            decrementedConflicts.Append("Decremented conflicts of cell (" + c.Row + ", " + c.Col + ") [" + c.Answer + "] to " + c.NumberOfConflicts + "\n");
#endif

                            if (c.NumberOfConflicts == 0)
                            {
                                this._conflicts.Remove(c.Row, c.Col);

#if DEBUG
                                decrementedConflicts.Append("\tCell removed from CellContainer\n");
#endif
                            }
                        }
                        else if (c.Answer == swapDestinationCell.Answer)
                        {
                            ++swapCellNewConflicts;
                            ++c.NumberOfConflicts;

#if DEBUG
                            incrementedConflicts.Append("Incremented conflicts of cell (" + c.Row + ", " + c.Col + ") [" + c.Answer + "] to " + c.NumberOfConflicts + "\n");
#endif

                            if (c.NumberOfConflicts == 1)
                            {
                                this._conflicts.Add(c);

#if DEBUG
                                incrementedConflicts.Append("\tCell inserted into CellContainer\n");
#endif
                            }
                        }
                    }

#if DEBUG
                    debugMessage.Append(decrementedConflicts.ToString() + "\n");
                    debugMessage.Append(incrementedConflicts.ToString() + "\n");
                    decrementedConflicts = new StringBuilder();
                    incrementedConflicts = new StringBuilder();
#endif

                    IList<Cell> swapCellVisibleCells = SudokuUtil.GetVisibleCellsModified(swapDestinationCell.Row, swapDestinationCell.Col, newPuzzle);
                    foreach (Cell c in swapCellVisibleCells)
                    {
                        if (c.Answer == swapDestinationCell.Answer)
                        {
                            --c.NumberOfConflicts;

#if DEBUG
                            decrementedConflicts.Append("Decremented conflicts of cell (" + c.Row + ", " + c.Col + ") [" + c.Answer + "] to " + c.NumberOfConflicts + "\n");
#endif

                            if (c.NumberOfConflicts == 0)
                            {
                                this._conflicts.Remove(c.Row, c.Col);

#if DEBUG
                                decrementedConflicts.Append("\tCell removed from CellContainer\n");
#endif
                            }
                        }
                        else if (c.Answer == currCell.Answer)
                        {
                            ++currCellNewConflicts;
                            ++c.NumberOfConflicts;

#if DEBUG
                            incrementedConflicts.Append("Incremented conflicts of cell (" + c.Row + ", " + c.Col + ") [" + c.Answer + "] to " + c.NumberOfConflicts + "\n");
#endif

                            if (c.NumberOfConflicts >= 1)
                            {
                                this._conflicts.Add(c);

#if DEBUG
                                incrementedConflicts.Append("\tCell inserted into CellContainer\n");
#endif
                            }
                        }
                    }

#if DEBUG
                    debugMessage.Append(decrementedConflicts.ToString() + "\n");
                    debugMessage.Append(incrementedConflicts.ToString() + "\n\n");
#endif

                    int temp = currCell.Answer;
                    currCell.Answer = swapDestinationCell.Answer;
                    swapDestinationCell.Answer = temp;

                    currCell.NumberOfConflicts = currCellNewConflicts;

#if DEBUG
                    debugMessage.Append("Updated Current Cell:\n\tPosition: (" + currCell.Row + ", " + currCell.Col +
                                        ")\n\tValue: " + currCell.Answer + "\n\tConflicts: " + currCell.NumberOfConflicts + "\n");
#endif

                    if (currCell.NumberOfConflicts == 0)
                    {
                        this._conflicts.Remove(currCell.Row, currCell.Col);

#if DEBUG
                        incrementedConflicts.Append("\tCell inserted into CellContainer\n");
#endif
                    }

                    swapDestinationCell.NumberOfConflicts = swapCellNewConflicts;

#if DEBUG
                    debugMessage.Append("\nUpdated Swap Cell:\n\tPosition: (" + swapDestinationCell.Row + ", " + swapDestinationCell.Col
                                        + ")\n\tValue: " + swapDestinationCell.Answer + "\n\tConflicts: " + swapDestinationCell.NumberOfConflicts + "\n");
#endif

                    if (swapDestinationCell.NumberOfConflicts == 0)
                    {
                        this._conflicts.Remove(swapDestinationCell.Row, swapDestinationCell.Col);

#if DEBUG
                        incrementedConflicts.Append("\tCell inserted into CellContainer\n");
#endif
                    }
                }
#if DEBUG
                else
                {
                    debugMessage.Append("No swap found for this iteration\n");
                }
#endif

                ++nIterations;
                
#if DEBUG
                debugMessage.Append("\nFinal Grid:\n\n" + newPuzzle);
                Debug.Write(debugMessage.ToString());

                if (nIterations > 100)
                {
                }
#endif
            }

            newPuzzle.NIterations = nIterations;
        }

        #endregion
    }
}