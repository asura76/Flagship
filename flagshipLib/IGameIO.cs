using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlagshipLib
{
    public interface IGameIO
    {
        void Save(string fileName, IGame game);

        void Load(string fileName, IGame game);

        void StringShipFactory(IShip shipToPlace, XmlReader read);
        //IGame theGame { get; set; }
    }
}
