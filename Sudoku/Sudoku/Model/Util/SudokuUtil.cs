﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Model.Grid;
using System.Collections.ObjectModel;
using Sudoku.ViewModel;

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
        /// Gets a list containing the tiles that are part of the specified row house.
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<Tile> GetRowHouse(int rowNum, SudokuGrid s)
        {
            List<Tile> house = new List<Tile>();

            for (int i = 0; i < 9; ++i)
            {
                house.Add(s.Tiles[rowNum][i]);
            }

            return house;
        }

        /// <summary>
        /// Gets a list containing the tiles that are part of the specified column house.
        /// </summary>
        /// <param name="colNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<Tile> GetColumnHouse(int colNum, SudokuGrid s)
        {
            List<Tile> house = new List<Tile>();

            for (int i = 0; i < 9; ++i)
            {
                house.Add(s.Tiles[i][colNum]);
            }

            return house;
        }

        /// <summary>
        /// Gets a list containing the tiles that are part of the specified square  house.
        /// </summary>
        /// <param name="squareNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<Tile> GetSquareHouse(int squareNum, SudokuGrid s)
        {
            // 0-1-2
            // 3-4-5
            // 6-7-8

            List<Tile> house = new List<Tile>();

            int rowStart = GetStartingRow(squareNum);
            int rowEnd = rowStart + 3;
            int colStart = GetStartingCol(squareNum);
            int colEnd = colStart + 3;

            for (int curRow = rowStart; curRow < rowEnd; ++curRow)
            {
                for (int curCol = colStart; curCol < colEnd; ++curCol)
                {
                    house.Add(s.Tiles[curRow][curCol]);
                }
            }

            return house;
        }

        /// <summary>
        /// Gets a list containing the tiles that are part of the specified square  house.
        /// </summary>
        /// <param name="squareNum"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static List<TileViewModel> GetSquareHouse(int squareNum, ObservableCollection<ObservableCollection<TileViewModel>> rows)
        {
            // 0-1-2
            // 3-4-5
            // 6-7-8

            List<TileViewModel> house = new List<TileViewModel>();

            int rowStart = GetStartingRow(squareNum);
            int rowEnd = rowStart + 3;
            int colStart = GetStartingCol(squareNum);
            int colEnd = colStart + 3;

            for (int curRow = rowStart; curRow < rowEnd; ++curRow)
            {
                for (int curCol = colStart; curCol < colEnd; ++curCol)
                {
                    house.Add(rows[curRow][curCol]);
                }
            }

            return house;
        }

        /// <summary>
        /// Method that returns the starting row of a square house (the row of the top-left tile of the house).
        /// </summary>
        /// <param name="squareNum"></param>
        /// <returns></returns>
        private static int GetStartingRow(int squareNum)
        {
            switch (squareNum)
            {
                case 0:
                case 1:
                case 2:
                    return 0;

                case 3:
                case 4:
                case 5:
                    return 3;

                default:
                    return 6;
            }
        }

        /// <summary>
        /// Method that returns the starting column of a square house (the column of the top-left tile of the house).
        /// </summary>
        /// <param name="squarNum"></param>
        /// <returns></returns>
        private static int GetStartingCol(int squareNum)
        {
            switch (squareNum)
            {
                case 0:
                case 3:
                case 6:
                    return 0;

                case 1:
                case 4:
                case 7:
                    return 3;

                default:
                    return 6;
            }
        }

        #endregion
    }
}