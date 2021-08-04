using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class FlagshipMap : Map
    {
        private const int DEFAULT_N_ROWS = 20;
        private const int DEFAULT_N_COLUMNS = 30;

        private FlagshipMap() : base(N_ROWS,
            N_COLUMNS)
        {
        }

        public static void SetMapSize(int nRows, int nColumns)
        {
            N_ROWS = nRows;
            N_COLUMNS = nColumns;
            theOneOnlyMap = null;
        }

        public static FlagshipMap GetInstance()
        {
            if (theOneOnlyMap == null)
            {
                theOneOnlyMap = new FlagshipMap();
            }
            return theOneOnlyMap;
        }

        public static void setDimensions(int nRows, int nColumns)
        {
            FlagshipMap.SetMapSize(nRows, nColumns);
            N_ROWS = nRows;
            N_COLUMNS = nColumns;
        }


        private static FlagshipMap theOneOnlyMap = null;
        private static int N_ROWS = DEFAULT_N_ROWS;
        private static int N_COLUMNS = DEFAULT_N_COLUMNS;
    }
}
