using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    public class Tile
    {
        #region Properties

        public short CurrentValue { get; set; }
        public short Answer { get; set; }

        #endregion

        #region Constructors

        public Tile()
        {
            this.CurrentValue = 0;
            this.Answer = 0;
        }

        public Tile(Tile t)
        {
            this.CurrentValue = t.CurrentValue;
            this.Answer = t.Answer;
        }

        #endregion

        #region Methods



        #endregion
    }
}
