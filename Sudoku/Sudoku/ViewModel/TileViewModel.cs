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

        private Tile _Tile { get; set; }

        /// <summary>
        /// The row of the tile.
        /// </summary>
        public int Row
        {
            get
            {
                return this._Tile.Row;
            }
        }

        /// <summary>
        /// The column of the tile.
        /// </summary>
        public int Col
        {
            get
            {
                return this._Tile.Col;
            }
        }

        /// <summary>
        /// The current value set by the user. Has a value of 0 if it is not set or if this tile is an answer tile.
        /// </summary>
        public int CurrentValue
        {
            get
            {
                return this._Tile.CurrentValue;
            }
            set
            {
                this._Tile.CurrentValue = value;
            }
        }

        /// <summary>
        /// The digit that goes in this tile in the current puzzle's solution.
        /// </summary>
        public int Answer
        {
            get
            {
                return this._Tile.Answer;
            }
            set
            {
                this._Tile.Answer = value;
            }
        }

        /// <summary>
        /// Whether the user can change the value of this tile. In other words, whether this tile's value is part of the initial puzzle.
        /// </summary>
        public Boolean IsModifiable
        {
            get
            {
                return this._Tile.IsModifiable;
            }
        }

        /// <summary>
        /// Whether the user has set a digit for this tile.
        /// </summary>
        public Boolean IsSet
        {
            get
            {
                return this._Tile.IsSet;
            }
            set
            {
                this._Tile.IsSet = value;
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
            this._Tile = t;
            this.NotifyPropertyChanged("Answer");
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
