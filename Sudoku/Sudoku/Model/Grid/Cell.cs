using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    /// <summary>
    /// Model class responsible for logic associated with a cell in the Sudoku puzzle's grid.
    /// </summary>
    public class Cell
    {
        #region Properties

        /// <summary>
        /// The row of the cell.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The column of the cell.
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// The current value set by the user. Has a value of 0 if it is not set or if this cell is an answer cell.
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// The digit that goes in this cell in the current puzzle's solution.
        /// </summary>
        public int Answer { get; set; }

        /// <summary>
        /// Whether the user can change the value of this cell. In other words, whether this cell's value is part of the initial puzzle.
        /// </summary>
        public Boolean IsModifiable { get; set; }

        /// <summary>
        /// Whether the user has set a digit for this cell.
        /// </summary>
        public Boolean IsSet { get; set; }

        /// <summary>
        /// The number of conflicting visible cells seen by this cell.
        /// </summary>
        public int NumberOfConflicts { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor that sets default values.
        /// </summary>
        public Cell()
        {
            this.CurrentValue = 0;
            this.Answer = 0;
            this.IsModifiable = false;
            this.IsSet = false;
        }

        /// <summary>
        /// Constructor that sets this Cell's Row and Column properties with specified values.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            this.CurrentValue = 0;
            this.Answer = 0;
            this.IsModifiable = false;
            this.IsSet = false;
        }

        /// <summary>
        /// Constructs this Cell by performing a deep copy of another Cell.
        /// </summary>
        /// <param name="t"></param>
        public Cell(Cell t)
        {
            this.Row = t.Row;
            this.Col = t.Col;
            this.CurrentValue = t.CurrentValue;
            this.Answer = t.Answer;
            this.IsModifiable = t.IsModifiable;
            this.IsSet = t.IsSet;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string toString = "(" + this.Row + ", " + this.Col + ")  [" + this.Answer + "]  " + this.NumberOfConflicts;

            return toString;
        }

        #endregion
    }
}