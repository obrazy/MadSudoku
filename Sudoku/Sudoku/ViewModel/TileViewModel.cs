using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Sudoku.Model.Grid;
using Sudoku.Model;

namespace Sudoku.ViewModel
{
    class TileViewModel : INotifyPropertyChanged
    {

        #region Properties

        // INotifyPropertyChanged related
        public event PropertyChangedEventHandler PropertyChanged;

        // Tile properties
        public Int16 Row { get; set; }
        public Int16 Col { get; set; }
        public Int16 CurrentValue { get; set; }
        public Int16 Answer { get; set; }
        public Boolean IsModifiable { get; set; }
        public Boolean IsSet { get; set; }

        #endregion

        #region Constructors

        public TileViewModel(Int16 row, Int16 col)
            : this(ModelFacade.Instance.CloneTile(row, col))
        {
        }

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

        // INotifyPropertyChanged related
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        #endregion

    }
}
