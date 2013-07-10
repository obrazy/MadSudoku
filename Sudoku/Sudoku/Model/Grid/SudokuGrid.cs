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
        #region temp

        public int NIterations { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Array of arrays of CellViewModel for the representation of the puzzle's cells.
        /// </summary>
        public Cell[][] Cells { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor that initiates the Cells property.
        /// </summary>
        public SudokuGrid()
        {
            this.Cells = new Cell[9][];

            for (int i = 0; i < 9; ++i)
            {
                this.Cells[i] = new Cell[9];
            }

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    this.Cells[i][j] = new Cell(i, j);
                }
            }
        }

        /// <summary>
        /// Constructs this SudokuGrid by performing a deep copy of another SudokuGrid. 
        /// </summary>
        /// <param name="s"></param>
        public SudokuGrid(SudokuGrid s)
        {
            this.Cells = new Cell[9][];

            for (int i = 0; i < 9; ++i)
            {
                this.Cells[i] = new Cell[9];
            }

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    this.Cells[i][j] = new Cell(s.Cells[i][j]);
                }
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string toString = string.Empty;

            for (int i = 0; i < 9; ++i)
            {
                if (i == 3 || i == 6)
                {
                    toString += "\n";
                }

                for (int j = 0; j < 9; ++j)
                {
                    if (j == 3 || j == 6)
                    {
                        toString += "  ";
                    }
                    toString += this.Cells[i][j].Answer;
                }
            }

            return toString;
        }

        #endregion
    }
}