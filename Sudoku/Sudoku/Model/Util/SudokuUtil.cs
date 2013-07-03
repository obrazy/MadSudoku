using System;
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
        /// Gets a list containing the cells that are part of the specified row house.
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<Cell> GetRowHouse(int rowNum, SudokuGrid s)
        {
            IList<Cell> house = new List<Cell>();

            for (int i = 0; i < 9; ++i)
            {
                house.Add(s.Cells[rowNum][i]);
            }

            return house;
        }

        /// <summary>
        /// Gets a list containing the cells that are part of the specified column house.
        /// </summary>
        /// <param name="colNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<Cell> GetColumnHouse(int colNum, SudokuGrid s)
        {
            IList<Cell> house = new List<Cell>();

            for (int i = 0; i < 9; ++i)
            {
                house.Add(s.Cells[i][colNum]);
            }

            return house;
        }

        /// <summary>
        /// Gets a list containing the cells that are part of the specified square house.
        /// </summary>
        /// <param name="squareNum"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<Cell> GetSquareHouse(int squareNum, SudokuGrid s)
        {
            // 0-1-2
            // 3-4-5
            // 6-7-8

            IList<Cell> house = new List<Cell>();

            int rowStart = GetStartingRow(squareNum);
            int rowEnd = rowStart + 3;
            int colStart = GetStartingCol(squareNum);
            int colEnd = colStart + 3;

            for (int curRow = rowStart; curRow < rowEnd; ++curRow)
            {
                for (int curCol = colStart; curCol < colEnd; ++curCol)
                {
                    house.Add(s.Cells[curRow][curCol]);
                }
            }

            return house;
        }

        /// <summary>
        /// Gets a list of the cells that are part of the square house containing the cell at the given row and column.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<Cell> GetSquareHouse(int row, int col, SudokuGrid s)
        {
            return GetSquareHouse((row / 3) * 3 + (col / 3), s);
        }

        /// <summary>
        /// Gets a list containing the cell view-models that are part of the specified square house.
        /// </summary>
        /// <param name="squareNum"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<CellViewModel> GetSquareHouse(int squareNum, ObservableCollection<ObservableCollection<CellViewModel>> rows)
        {
            // 0-1-2
            // 3-4-5
            // 6-7-8

            IList<CellViewModel> house = new List<CellViewModel>();

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
        /// Method that returns the starting row of a square house (the row of the top-left cell of the house).
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
        /// Method that returns the starting column of a square house (the column of the top-left cell of the house).
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

        /// <summary>
        /// /// Gets a list of all the cells that are visible to the cell at the location given by the row and col arguments.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<Cell> GetVisibleCells(int row, int col, SudokuGrid s)
        {
            return null;
        }

        /// <summary>
        /// Gets a list of the cells that are visible to the cell at the location given by the row and col arguments,
        /// excluding the cells within the same row house.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<Cell> GetVisibleCellsModified(int row, int col, SudokuGrid s)
        {
            IList<Cell> visibleCells = GetColumnHouse(col, s);

            foreach (Cell c in GetSquareHouse(row, col, s))
            {
                if (c.Row != row && c.Col != col)
                {
                    visibleCells.Add(c);
                }
            }

            return visibleCells;
        }
        
        #endregion
    }
}