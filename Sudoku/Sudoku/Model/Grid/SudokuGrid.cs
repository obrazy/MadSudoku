using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model;

namespace Sudoku.Model.Grid
{
    public class SudokuGrid
    {
        #region Properties

        public Tile[,] Tiles { get; set; }
        public string Name { get; set; }

        #endregion

        #region Constructors

        public SudokuGrid()
        {
            this.Tiles = new Tile[9,9];
            foreach (Tile t in this.Tiles)
            {
                t.CurrentValue = 0;
            }
            this.Name = "yo";
        }

        #endregion

    }
}
