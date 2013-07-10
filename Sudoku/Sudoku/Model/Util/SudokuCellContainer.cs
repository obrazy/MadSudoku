using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Sudoku.Model.Grid;

namespace Sudoku.Model.Util
{
    /// <summary>
    /// Data structure class to contain objects of type Cell. Offers access, insert, delete and
    /// random operations in O(1) time.
    /// </summary>
    public class SudokuCellContainer
    {
        #region Properties

        /// <summary>
        /// Dictionary. The keys are of type Tuple<int, int>, representing the coordinates of a cell
        /// in a Sudoku grid, and the values are the index in this._list of the Cell object that has
        /// those coordinates.
        /// </summary>
        private Dictionary<Tuple<int, int>, int> _dict;

        /// <summary>
        /// List containing elements of type Cell. Cells in this list are cells that have conflicting
        /// visible cells.
        /// </summary>
        private IList<Cell> _list;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random _rng;        

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor.
        /// </summary>
        public SudokuCellContainer()
        {
            this._dict = new Dictionary<Tuple<int, int>, int>();
            this._list = new List<Cell>();
            this._rng = new Random();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs insert operation into this data structure. Does nothing if the specified cell has
        /// already been inserted or if it has no conflicts.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Add(Cell cell)
        {
            var key = Tuple.Create(cell.Row, cell.Col);

            if (cell.NumberOfConflicts == 0 || this._dict.ContainsKey(key))
            {
                return;
            }
            else
            {
                this._list.Add(cell);
                this._dict[key] = this._list.Count - 1;
            }
        }

        /// <summary>
        /// Removes the cell that has the specified row and column coordinates from this data structure.
        /// If this data structure does not contain that cell, this method does nothing.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(int row, int col)
        {
            var key = Tuple.Create(row, col);

            if (this._dict.ContainsKey(key))
            {
                if (this._list.Count <= 1)
                {
                    this._list.Clear();
                    this._dict.Clear();
                }
                else
                {
                    int removedIndex = this._dict[key];
                    int lastListIndex = this._list.Count - 1;

                    if (removedIndex != lastListIndex)
                    {
                        Cell movedCell = this._list[lastListIndex];
                        this._list[removedIndex] = this._list[lastListIndex];
                        var movedCellKey = Tuple.Create(movedCell.Row, movedCell.Col);
                        this._dict[movedCellKey] = removedIndex;
                    }

                    this._list.RemoveAt(lastListIndex);
                    this._dict.Remove(key);
                }
            }
        }

        /// <summary>
        /// Gets the cell that has the specified row and column coordinates.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Cell Get(int row, int col)
        {
            var key = Tuple.Create(row, col);
            return this._list[this._dict[key]];
        }

        /// <summary>
        /// Gets a random entry from this data structure.
        /// </summary>
        /// <returns></returns>
        public Cell GetRandomCell()
        {
            return this._list[this._rng.Next(this._list.Count)];
        }

        /// <summary>
        /// Returns whether or not this data structure is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this._list.Count == 0;
        }

        #endregion
    }
}