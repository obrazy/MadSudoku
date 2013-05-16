﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    public class SudokuGrid
    {
        #region Properties

        public Tile[,] Tiles { get; set; }

        #endregion

        #region Constructors

        public SudokuGrid()
        {
            this.Tiles = new Tile[9, 9];
        }

        public SudokuGrid(SudokuGrid s)
        {
            this.Tiles = new Tile[9, 9];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Tiles[i, j] = new Tile(s.Tiles[i, j]);
                }
            }
        }

        #endregion

        #region Methods



        #endregion

    }
}
