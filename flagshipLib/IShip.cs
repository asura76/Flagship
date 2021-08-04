using System.Collections.Generic;
using System.Drawing;

namespace FlagshipLib
{
    public interface IShip : IMapObject
    {
        ShipSquare this[int offsetFromFront] { get; }
        ShipSquare this[int row, int column] { get; }
        string Name { get; set; }
        // Class indicates the type of ship 
        //(e.g., "Aircraft Carrier", "Battleship, etc)
        string Class { get; }
        int NavyId { get; set; }
        int LengthInSquares { get;  }
        Bitmap Image { get; }

        bool Destroyed { get; }
        double Speed { get; }
        Direction  Direction { get;  }
        int Column { get; set; }
        bool Invincibility { get; set; }
        IMessageReceiver MessageReceiver { get; set; }
        int Row { get; set; }

        ShipSquare[] shipsquares_ { get; set; }
        int NTurnsTilMove { get; set; }
        bool Attack(int row, int column);
        void changeDir(Direction.CompassPoint direction, int nSteps);
        void changeDirByString(string direction, int nSteps);
        void CollideWith(IMapObject thingColliding, int row, int column);
        bool Contains(int row, int column);
        IEnumerator<ShipSquare> GetEnumerator();
        //bool isDestroyed(int row, int column);
        void PostTimeStep();
        void PreTimeStep();
        void TimeStep();
        bool IsShipDestroyed();
        List<ShipSquare> squaresPendingDestruction { get; set; }
        bool destroyed_ { get; set; }
    }
}