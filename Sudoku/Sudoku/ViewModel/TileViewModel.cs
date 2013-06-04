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
    public class TileViewModel : ViewModelBase
    {
        #region Properties

        private Tile _tile { get; set; }

        /// <summary>
        /// The row of the tile.
        /// </summary>
        public int Row
        {
            get
            {
                return this._tile.Row;
            }
        }

        /// <summary>
        /// The column of the tile.
        /// </summary>
        public int Col
        {
            get
            {
                return this._tile.Col;
            }
        }

        /// <summary>
        /// The current value set by the user. Has a value of 0 if it is not set or if this tile is an answer tile.
        /// </summary>
        public int CurrentValue
        {
            get
            {
                return this._tile.CurrentValue;
            }
            set
            {
                this._tile.CurrentValue = value;
                this.NotifyPropertyChanged("CurrentValue");
            }
        }

        /// <summary>
        /// The digit that goes in this tile in the current puzzle's solution.
        /// </summary>
        public int Answer
        {
            get
            {
                return this._tile.Answer;
            }
            set
            {
                this._tile.Answer = value;
                this.NotifyPropertyChanged("Answer");
            }
        }

        /// <summary>
        /// Whether the user can change the value of this tile. In other words, whether this tile's value is part of the initial puzzle.
        /// </summary>
        public Boolean IsModifiable
        {
            get
            {
                return this._tile.IsModifiable;
            }
        }

        /// <summary>
        /// Whether the user has set a digit for this tile.
        /// </summary>
        public Boolean IsSet
        {
            get
            {
                return this._tile.IsSet;
            }
            set
            {
                this._tile.IsSet = value;
                this.NotifyPropertyChanged("IsSet");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs this view-model by requesting a clone of its associated tile to the ModelFacade.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public TileViewModel(int row, int col)
            : this(ModelFacade.Instance.CloneTile(row, col))
        {
        }

        /// <summary>
        /// Constructs this view-model using its associated tile's properties.
        /// </summary>
        /// <param name="t"></param>
        public TileViewModel(Tile t)
        {
            this._tile = t;

            //this.Row = t.Row;
            //this.Col = t.Col;
            //this.CurrentValue = t.CurrentValue;
            //this.Answer = t.Answer;
            //this.IsModifiable = t.IsModifiable;
            //this.IsSet = t.IsSet;
        }

        #endregion

        #region Methods
        #endregion
    }
}
