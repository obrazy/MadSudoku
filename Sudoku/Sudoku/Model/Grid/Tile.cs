﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    public class Tile
    {
        #region Properties

        public Int16 CurrentValue { get; set; }
        public Int16 Answer { get; set; }
        public Boolean IsModifiable { get; set; }

        #endregion

        #region Constructors

        public Tile()
        {
            this.CurrentValue = 0;
            this.Answer = 0;
            this.IsModifiable = false;
        }

        public Tile(Tile t)
        {
            this.CurrentValue = t.CurrentValue;
            this.Answer = t.Answer;
        }

        public Tile(Int16 answer)
        {

        }

        #endregion

        #region Methods



        #endregion
    }
}
