using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FlagshipLib
{
    public interface IPlayer : IMessageReceiver
    {
        // Send a particular kind of ship to the player
        // Use the object's information about itself to record location,
        // heading and velocity.  Upon return the Game function will 
        // place the ship on the map.
        void PlaceShip(IShip shipToPlace);

        void FakeRequestCommand(IShip shipToRecieveOrders, string direction,
            int rowToAttack, int columnToAttack);

        void RequestCommand(IShip shipToReceiveOrders);

        void RequestMissleTarget(int row, int column);

        // Get the list of ships in the player's navy
        bool NavyDestroyed();

        void SetIntAnswer(int answer);

        void ReceiveMessage(string message);

        IShip getShipByName(string shipName);

        void PreTimeStep();

        void TimeStep();

        void PostTimeStep();

        List<IShip> Navy { get; set; }
        string Name { get; set; }
        int NavyId { get; set; }
        List<IShip> ShipsOnMap { get; set; }

        //attacksonmap shows the attemted attacks on the map
        List<Tuple<Point,int>> MissesOnMap { get; set; }
        //hitsonmap shows the successful attack on map
        List<Tuple<Point, int>> HitsOnMap { get; set; }
        Color NavyColor { get; set; }
        List<string> MessageString { get; set; }

    }
}
