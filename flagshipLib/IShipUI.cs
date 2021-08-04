using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public interface IShipUI
    {
        void placeShip(IShip shipToPlace, IPlayer player);
    }
}
