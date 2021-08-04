using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class BattleShip : Ship
    {
        public BattleShip(string name = "Unknown", int id = 0) : 
            base(0, 0, 2, Direction.CompassPoint.North, 4)
        {
            Class = "BattleShip";
            //Image = new System.Drawing.Bitmap("../battleship.jpg");
            Image = new System.Drawing.Bitmap(@"C:\Users\Adjua\source\repos\Flagship4\flagship\battleshippng.png");
        }

        public BattleShip(int row, int column, Direction.CompassPoint direction, 
            string name=Ship.UnknownShip, int id=Ship.UnknownNavy) 
            : base(row, column,2 , direction, 4, name, id)
        {
            Class = "BattleShip";
        }
    }
}
