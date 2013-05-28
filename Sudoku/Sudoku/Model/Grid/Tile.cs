using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    /// <summary>
    /// Model class responsible for logic associated with a tile in the Sudoku puzzle's grid.
    /// </summary>
    public class Tile
    {
        #region Properties

        /// <summary>
        /// The row of the tile.
        /// </summary>
        public Int16 Row { get; set; }

        /// <summary>
        /// The column of the tile.
        /// </summary>
        public Int16 Col { get; set; }

        /// <summary>
        /// The current value set by the user. Has a value of 0 if it is not set or if this tile is an answer tile.
        /// </summary>
        public Int16 CurrentValue { get; set; }

        /// <summary>
        /// The digit that goes in this tile in the current puzzle's solution.
        /// </summary>
        public Int16 Answer { get; set; }

        /// <summary>
        /// Whether the user can change the value of this tile. In other words, whether this tile's value is part of the initial puzzle.
        /// </summary>
        public Boolean IsModifiable { get; set; }

        /// <summary>
        /// Whether the user has set a digit for this tile.
        /// </summary>
        public Boolean IsSet { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor that sets default values.
        /// </summary>
        public Tile()
        {
            this.CurrentValue = 0;
            this.Answer = 0;
            this.IsModifiable = false;
            this.IsSet = false;
        }

        /// <summary>
        /// Constructor that sets this Tile's Row and Column properties with specified values.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Tile(Int16 row, Int16 col)
        {
            this.Row = row;
            this.Col = col;
            this.CurrentValue = 0;
            this.Answer = 0;
            this.IsModifiable = false;
            this.IsSet = false;
        }

        /// <summary>
        /// Constructs this Tile by performing a deep copy of another Tile.
        /// </summary>
        /// <param name="t"></param>
        public Tile(Tile t)
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
        #endregion
    }
}
