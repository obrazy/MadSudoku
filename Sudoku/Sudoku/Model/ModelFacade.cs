using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Model
{
    public class ModelFacade
    {
        #region Properties

        private static ModelFacade _Instance;

        public static ModelFacade Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ModelFacade();
                }
                return _Instance;
            }
        }

        #endregion

        #region Constructors

        private ModelFacade() { }

        #endregion
    }
}
