//#define DEBUG
#undef DEBUG
#define TIMING
//#undef TIMING


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
#if TIMING
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif

            SudokuGrid newPuzzle = new SudokuGrid();

            FillOneToNine(newPuzzle);
            InitializeConflicts(newPuzzle);
            MinConflicts(newPuzzle);

#if TIMING
            sw.Stop();
            System.Console.Write("\nGenerated in " + sw.ElapsedMilliseconds + " milliseconds\n\n");
#endif

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
            StringBuilder removedCells;
            StringBuilder insertedCells;
            int nIterations = 0;
#endif

            while (!this._conflicts.IsEmpty())
            {
                Cell currCell = this._conflicts.GetRandomCell();

#if DEBUG
                debugMessage = new StringBuilder();
                debugMessage.Append("\n=========================================\n\n");
                debugMessage.Append("Iteration: " + nIterations + "\n\n");
                debugMessage.Append("Initial Grid:\n\n" + newPuzzle);
                debugMessage.Append("\n\nInitial Current Cell: " + currCell + "\n\n");
                removedCells = new StringBuilder();
                removedCells.Append("Cells removed from CellContainer this iteration:\n");
                insertedCells = new StringBuilder();
                insertedCells.Append("Cells inserted into CellContainer this iteration:\n");
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

                    if ((currCellFinalConflicts + newCellFinalConflicts) <= (currCellInitialConflicts + newCellInitialConflicts))
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
                    debugMessage.Append("Initial Swap Cell: " + swapDestinationCell + "\n\n\n");
                    StringBuilder decrementedConflicts = new StringBuilder();
                    decrementedConflicts.Append("Cells visible to CurrentCell that had their conflicts decremented:\n");
                    StringBuilder incrementedConflicts = new StringBuilder();
                    incrementedConflicts.Append("Cells visible to CurrentCell that had their conflicts incremented:\n");
#endif

                    foreach (Cell c in currCellVisibleCells)
                    {
                        if (c.Answer == currCell.Answer)
                        {
                            --c.NumberOfConflicts;

#if DEBUG
                            decrementedConflicts.Append("\t" + c + "\n");
#endif

                            if (c.NumberOfConflicts == 0)
                            {
                                this._conflicts.Remove(c.Row, c.Col);

#if DEBUG
                                removedCells.Append("\t" + c + "\n");
#endif
                            }
                        }
                        else if (c.Answer == swapDestinationCell.Answer)
                        {
                            ++currCellNewConflicts;
                            ++c.NumberOfConflicts;

#if DEBUG
                            incrementedConflicts.Append("\t" + c + "\n");
#endif

                            if (c.NumberOfConflicts == 1)
                            {
                                this._conflicts.Add(c);

#if DEBUG
                                insertedCells.Append("\t" + c + "\n");
#endif
                            }
                        }
                    }

#if DEBUG
                    debugMessage.Append(decrementedConflicts.ToString() + "\n");
                    debugMessage.Append(incrementedConflicts.ToString() + "\n");
                    decrementedConflicts = new StringBuilder();
                    decrementedConflicts.Append("Cells visible to SwapCell that had their conflicts decremented:\n");
                    incrementedConflicts = new StringBuilder();
                    incrementedConflicts.Append("Cells visible to SwapCell that had their conflicts incremented:\n");
#endif

                    IList<Cell> swapCellVisibleCells = SudokuUtil.GetVisibleCellsModified(swapDestinationCell.Row, swapDestinationCell.Col, newPuzzle);
                    foreach (Cell c in swapCellVisibleCells)
                    {
                        if (c.Answer == swapDestinationCell.Answer)
                        {
                            --c.NumberOfConflicts;

#if DEBUG
                            decrementedConflicts.Append("\t" + c + "\n");
#endif

                            if (c.NumberOfConflicts == 0)
                            {
                                this._conflicts.Remove(c.Row, c.Col);

#if DEBUG
                                removedCells.Append("\t" + c + "\n");
#endif
                            }
                        }
                        else if (c.Answer == currCell.Answer)
                        {
                            ++swapCellNewConflicts;
                            ++c.NumberOfConflicts;

#if DEBUG
                            incrementedConflicts.Append("\t" + c + "\n");
#endif

                            if (c.NumberOfConflicts == 1)
                            {
                                this._conflicts.Add(c);

#if DEBUG
                                insertedCells.Append("\t" + c + "\n");
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
                    debugMessage.Append("Updated Current Cell: " + currCell + "\n\n");
#endif

                    if (currCell.NumberOfConflicts == 0)
                    {
                        this._conflicts.Remove(currCell.Row, currCell.Col);

#if DEBUG
                        removedCells.Append("\t" + currCell + "\n");
#endif
                    }

                    swapDestinationCell.NumberOfConflicts = swapCellNewConflicts;

#if DEBUG
                    debugMessage.Append("Updated Swap Cell: " + swapDestinationCell + "\n\n\n");
#endif

                    if (swapDestinationCell.NumberOfConflicts == 0)
                    {
                        this._conflicts.Remove(swapDestinationCell.Row, swapDestinationCell.Col);

#if DEBUG
                        removedCells.Append("\t" + swapDestinationCell + "\n");
#endif
                    }

#if DEBUG
                    debugMessage.Append(removedCells.ToString() + "\n");
                    debugMessage.Append(insertedCells.ToString() + "\n\n");
#endif
                }
#if DEBUG
                else
                {
                    debugMessage.Append("No swap found for this iteration\n\n");
                }

                debugMessage.Append("Final Grid:\n\n" + newPuzzle);
                System.Diagnostics.Debug.Write(debugMessage.ToString());

                if (nIterations > 150)
                {
                }

                ++nIterations;
#endif
            }
        }

        #endregion
    }
}