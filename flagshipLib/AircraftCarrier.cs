using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class AircraftCarrier : Ship
    {
        public AircraftCarrier(string name = "Unknown", int id = 0) :
            base(0, 0, 2, Direction.CompassPoint.North, 5)
        {
            Class = "Aircraft Carrier";
        }

        public AircraftCarrier(int row, int column, 
            Direction.CompassPoint direction, string name=Ship.UnknownShip, 
            int id=Ship.UnknownNavy) : 
            base(row, column, 1, direction, 5, name, id)
        {
            Class = "Aircraft Carrier";
        }
    }
}
