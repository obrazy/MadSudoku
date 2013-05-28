using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using Sudoku.Model;

namespace Sudoku.ViewModel
{
    /// <summary>
    /// View-model class associated with the Tile model class.
    /// </summary>
    class TileViewModel : ViewModelBase
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
        /// Constructs this view-model by requesting a clone of its associated tile to the ModelFacade.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public TileViewModel(Int16 row, Int16 col)
            : this(ModelFacade.Instance.CloneTile(row, col))
        {
        }

        /// <summary>
        /// Constructs this view-model using its associated tile's properties.
        /// </summary>
        /// <param name="t"></param>
        public TileViewModel(Tile t)
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
