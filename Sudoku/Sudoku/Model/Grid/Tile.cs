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
    }
}
