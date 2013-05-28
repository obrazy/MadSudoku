using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;

namespace Sudoku.Model.Util
{
    /// <summary>
    /// CLass containing various utility methods to assist the other modules.
    /// </summary>
    public class SudokuUtil
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods

        /// <summary>
        /// Gets a list containing the tiles that are part of the specified Row House.
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Tile> GetRowHouse(Int16 rowNum, SudokuGrid s)
        {
            List<Tile> house = new List<Tile>();

            return house;
        }

        /// <summary>
        /// Gets a list containing the tiles that are part of the specified Column House.
        /// </summary>
        /// <param name="colNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Tile> GetColumnHouse(Int16 colNum, SudokuGrid s)
        {
            List<Tile> house = new List<Tile>();

            return house;
        }

        /// <summary>
        /// Gets a list containing the tiles that are part of the specified Square House.
        /// </summary>
        /// <param name="squareNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Tile> GetSquareHouse(Int16 squareNum, SudokuGrid s)
        {
            // 1-2-3
            // 4-5-6
            // 7-8-9

            List<Tile> house = new List<Tile>();

            return house;
        }

        #endregion
    }
}