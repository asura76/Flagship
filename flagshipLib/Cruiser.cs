using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class Cruiser : Ship
    {
        public Cruiser(string name = "Unknown", int id = 0) : 
            base(0, 0, 3, Direction.CompassPoint.North, 2)
        {
            Class = "Cruiser";
        }

        public Cruiser(int row, int column, Direction.CompassPoint direction, 
            string name="Unknown", int id=0) : 
            base(row, column, 3, direction, 2, name, id) 
        {
            
            Class = "Cruiser";
            Name = name;
        }

    }
}
