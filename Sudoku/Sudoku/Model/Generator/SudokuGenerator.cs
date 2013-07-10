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
            int nIterations = 0;
            while (!this._conflicts.IsEmpty())
            {
                Cell currCell = this._conflicts.GetRandomCell();

                // Initialiser la structure de données pour conserver les scores
                IList<Tuple<int, int>> scores = new List<Tuple<int, int>>();    // <swapPosition, numberOfConflicts>

                // Évaluer la situation initiale pour la case sélectionnée et conserver le résultat
                IList<Cell> currCellVisibleCells = SudokuUtil.GetVisibleCellsModified(currCell.Row, currCell.Col, newPuzzle);
                int currCellInitialConflicts = currCellVisibleCells.Count(x => x.Answer == currCell.Answer);

                foreach (Cell c in SudokuUtil.GetRowHouse(currCell.Row, newPuzzle))
                {
                    if (c.Row == currCell.Row && c.Col == currCell.Col)
                    {
                        continue;
                    }

                    // Évaluer la situation initiale pour la nouvelle case
                    IList<Cell> newCellVisibleCells = SudokuUtil.GetVisibleCellsModified(c.Row, c.Col, newPuzzle);
                    int newCellInitialConflicts = newCellVisibleCells.Count(x => x.Answer == c.Answer);

                    // Évaluer la nouvelle situation pour la case choisie
                    int currCellFinalConflicts = newCellVisibleCells.Count(x => x.Answer == currCell.Answer);

                    // Évaluer la nouvelle situation pour la nouvelle case
                    int newCellFinalConflicts = currCellVisibleCells.Count(x => x.Answer == c.Answer);

                    // Si le score total de la nouvelle situation est meilleur ou égal au meilleur score actuel, ajuster la structure de données de scores
                    if ((currCellFinalConflicts + newCellFinalConflicts) < (currCellInitialConflicts + newCellInitialConflicts))
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
                    // Sélectionner au hasard une position d'échange parmi celles qui ont le meilleur score
                    int swapIndex = scores[this._rng.Next(scores.Count)].Item1;
                    Cell swapDestinationCell = newPuzzle.Cells[currCell.Row][swapIndex];

                    // Parmi les cases visibles depuis la position de la case choisie, décrémenter de 1 le nombre de conflits pour les cases qui ont la même valeur
                    // que la case choisie et incrémenter de 1 le nombre de conflits pour les cases qui ont la même valeur que la nouvelle case; si les cases dont
                    // le nombre de conflits a diminué ont maintenant 0 conflits, les retirer de la structure de données des conflits
                    foreach (Cell c in currCellVisibleCells)
                    {
                        if (c.Answer == currCell.Answer)
                        {
                            --c.NumberOfConflicts;
                            if (c.NumberOfConflicts == 0)
                            {
                                this._conflicts.Remove(c.Row, c.Col);
                            }
                        }
                        else if (c.Answer == swapDestinationCell.Answer)
                        {
                            ++c.NumberOfConflicts;
                            if (c.NumberOfConflicts == 1)
                            {
                                this._conflicts.Add(c);
                            }
                        }
                    }

                    // Parmi les cases visibles depuis la position de la nouvelle case, décrémenter de 1 le nombre de conflits pour les cases qui ont la même valeur
                    // que la nouvelle case et incrémenter de 1 le nombre de conflits pour les cases qui ont la même valeur que la case choisie; si les cases dont
                    // le nombre de conflits a diminué ont maintenant 0 conflits, les retirer de la structure de données des conflits
                    foreach (Cell c in SudokuUtil.GetVisibleCellsModified(swapDestinationCell.Row, swapDestinationCell.Col, newPuzzle))
                    {
                        if (c.Answer == swapDestinationCell.Answer)
                        {
                            --c.NumberOfConflicts;
                            if (c.NumberOfConflicts == 0)
                            {
                                this._conflicts.Remove(c.Row, c.Col);
                            }
                        }
                        else if (c.Answer == currCell.Answer)
                        {
                            ++c.NumberOfConflicts;
                        }
                    }

                    // Effectuer l'échange
                    int temp = currCell.Answer;
                    currCell.Answer = swapDestinationCell.Answer;
                    swapDestinationCell.Answer = temp;
                }

                ++nIterations;
            }

            newPuzzle.NIterations = nIterations;
        }

        #endregion
    }
}