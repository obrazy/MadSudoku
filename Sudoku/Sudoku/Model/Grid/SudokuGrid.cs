using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model.Grid
{
    /// <summary>
    /// Model class responsible for logic associated with the Sudoku puzzle's grid.
    /// </summary>
    public class SudokuGrid
    {
        #region Properties

        /// <summary>
        /// Array of arrays of TileVieModel for the representation of the puzzle's tiles.
        /// </summary>
        public Tile[][] Tiles { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor that initiates the Tiles property.
        /// </summary>
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

        /// <summary>
        /// Constructs this SudokuGrid by performing a deep copy of another SudokuGrid. 
        /// </summary>
        /// <param name="s"></param>
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
