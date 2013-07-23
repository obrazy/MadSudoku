using Sudoku.Model;
using Sudoku.Model.Grid;

namespace Sudoku.ViewModel
{
    /// <summary>
    /// View-model class associated with the Cell model class.
    /// </summary>
    public class CellViewModel : ViewModelBase
    {
        #region Properties

        private Cell _cell { get; set; }

        /// <summary>
        /// The row of the cell.
        /// </summary>
        public int Row
        {
            get
            {
                return this._cell.Row;
            }
        }

        /// <summary>
        /// The column of the cell.
        /// </summary>
        public int Col
        {
            get
            {
                return this._cell.Col;
            }
        }

        /// <summary>
        /// The current value set by the user. Has a value of 0 if it is not set or if this cell is an answer cell.
        /// </summary>
        public int CurrentValue
        {
            get
            {
                return this._cell.CurrentValue;
            }
            set
            {
                this._cell.CurrentValue = value;
                this.NotifyPropertyChanged("CurrentValue");
            }
        }

        /// <summary>
        /// The digit that goes in this cell in the current puzzle's solution.
        /// </summary>
        public int Answer
        {
            get
            {
                return this._cell.Answer;
            }
            set
            {
                this._cell.Answer = value;
                this.NotifyPropertyChanged("Answer");
            }
        }

        /// <summary>
        /// Whether the user can change the value of this cell. In other words, whether this cell's value is part of the initial puzzle.
        /// </summary>
        public bool IsModifiable
        {
            get
            {
                return this._cell.IsModifiable;
            }
        }

        /// <summary>
        /// Whether the user has set a digit for this cell.
        /// </summary>
        public bool IsSet
        {
            get
            {
                return this._cell.IsSet;
            }
            set
            {
                this._cell.IsSet = value;
                this.NotifyPropertyChanged("IsSet");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs this view-model by requesting a clone of its associated cell to the ModelFacade.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public CellViewModel(int row, int col)
            : this(ModelFacade.Instance.CloneCell(row, col))
        {
        }

        /// <summary>
        /// Constructs this view-model using its associated cell's properties.
        /// </summary>
        /// <param name="t"></param>
        public CellViewModel(Cell t)
        {
            this._cell = t;

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