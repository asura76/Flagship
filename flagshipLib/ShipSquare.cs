using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class ShipSquare
    {
        public int OffsetFromFront { get; set; }
        public Ship SquaresShip { get; set; }
        public bool Hit;

        public int correctForWrapAround(int coordinate, int dimension)
        {
            if (coordinate >= dimension) coordinate = coordinate - dimension;
            if (coordinate < 0) coordinate = dimension + coordinate;
            return coordinate;
        }

        public int Row
        {
            get
            {
                int row = SquaresShip.Row;
                //int column = SquaresShip.Column;
                if (SquaresShip.Direction.Current ==
                    Direction.CompassPoint.North) row = row - OffsetFromFront;
                if (SquaresShip.Direction.Current ==
                    Direction.CompassPoint.South) row = row + OffsetFromFront;

                return correctForWrapAround(row, SquaresShip.mapRef.NRows);
            }
            set { }
        }
        public int Column
        {
            get
            {
                int column = SquaresShip.Column;
                if (SquaresShip.Direction.Current ==
                    Direction.CompassPoint.West) column = column - OffsetFromFront;
                if (SquaresShip.Direction.Current ==
                    Direction.CompassPoint.East) column = column + OffsetFromFront;
                return correctForWrapAround(column, SquaresShip.mapRef.NColumns);
            }
            set { }
        }

        public void TimeStep()
        {

        }
    }
}
