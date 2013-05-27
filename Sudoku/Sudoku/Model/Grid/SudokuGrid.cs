using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    public class SudokuGrid
    {
        #region Properties

        public Tile[][] Tiles { get; set; }

        #endregion

        #region Constructors

        public SudokuGrid()
        {
            this.Tiles = new Tile[9][];

            for (Int16 i = 0; i < 9; ++i)
            {
                this.Tiles[i] = new Tile[9];
            }

            for (Int16 i = 0; i < 9; ++i)
            {
                for (Int16 j = 0; j < 9; ++j)
                {
                    this.Tiles[i][j] = new Tile(i, j);
                }
            }
        }

        public SudokuGrid(SudokuGrid s)
        {
            this.Tiles = new Tile[9][];

            for (Int16 i = 0; i < 9; ++i)
            {
                this.Tiles[i] = new Tile[9];
            }

            for (Int16 i = 0; i < 9; ++i)
            {
                for (Int16 j = 0; j < 9; ++j)
                {
                    this.Tiles[i][j] = new Tile(s.Tiles[i][j]);
                }
            }
        }

        #endregion

        #region Methods



        #endregion

    }
}
