using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FlagshipLib
{
    [Serializable]
    public class Player : IPlayer
    {
        private static int nextNavyId = 1;
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public int NavyId { get; set; }

        //attacksonmap shows the attemted attacks on the map
        public List<Tuple<Point, int>> MissesOnMap { get; set; }
        //hitsonmap shows the successful attack on map
        public List<Tuple<Point, int>> HitsOnMap { get; set; }

        public AircraftCarrier airship;
        public BattleShip battleShip;
        public Cruiser cruiser;

        public List<IShip> Navy { get; set; }
        public const string UnknownID = "Unknown ID";
        public const string UnknownName = "Unknown name";
        public List<IShip> ShipsOnMap { get; set; }
        public Color NavyColor { get; set; }
        public List<string> MessageString { get; set; }

        public Player(IGame theGame, string name=UnknownName, string navyID=UnknownID)
        {
            Navy = new List<IShip>();
            currentGame = theGame;

            MissesOnMap = new List<Tuple<Point, int>>();
            HitsOnMap = new List<Tuple<Point, int>>();

            NavyId = nextNavyId++;

            List<IShip> ShipsOnMap = new List<IShip>();

            MessageString = new List<string>();
        }

        public IShip getShipByName(string shipName)
        {
            int nNavy = 0;
            while (Navy[nNavy].Name != shipName)
            {
                if (Navy[nNavy].Name != shipName)
                    ++nNavy;
            }

            return Navy[nNavy];
        }

        public IShip this[IShip shipToFind]
        {
            get
            {
                int counter = 0;
                while(counter != Navy.Count &&
                    Navy[counter] != shipToFind)
                {
                    ++counter;
                }
                return Navy[counter];
            }
        }

        public void PlaceShip(IShip ship)
        {
            Navy.Add(ship);

        }



        public bool NavyDestroyed()
        {
            int nShips = 0;
            bool lost = false;

            foreach (Ship ship in Navy)
            {
                if (ship.IsShipDestroyed() == true)
                {
                    nShips += 1;
                }
            }
            if (nShips == Navy.Count) { lost = true; }

            return lost;
        }

        public void SetIntAnswer(int answer)
        {
            answer = Int32.Parse(Console.ReadLine());
        }

        public void setStringAnswer(string answer)
        {
            answer = Console.ReadLine();
        }

        //Implements same actions as RequestCommand
        // without Console
        public void FakeRequestCommand(IShip shipToRecieveOrders, string direction,
            int rowToAttack, int columnToAttack)
        {
            this[shipToRecieveOrders].changeDirByString(direction, (int)shipToRecieveOrders.Speed);
            
            RequestMissleTarget(rowToAttack, columnToAttack);
        }


        public void RequestCommand(IShip shipToReceiveOrders)
        {
            if(shipToReceiveOrders.Destroyed != true)
            {
                //select direction
                //shipToReceiveOrders.Direction = (player input)
                String playerCommand;
                Console.WriteLine("Direction: ");
                playerCommand = Console.ReadLine();
                switch (playerCommand)
                {
                    case "North":
                        shipToReceiveOrders.Direction.
                            RequestChange(Direction.CompassPoint.North, 
                            (int)shipToReceiveOrders.Speed);
                        break;
                    case "South":
                        shipToReceiveOrders.Direction.
                            RequestChange(Direction.CompassPoint.North,
                            (int)shipToReceiveOrders.Speed);
                        break;
                    case "East":
                        shipToReceiveOrders.Direction.
                            RequestChange(Direction.CompassPoint.North,
                            (int)shipToReceiveOrders.Speed);
                        break;
                    case "West":
                        shipToReceiveOrders.Direction.
                            RequestChange(Direction.CompassPoint.North,
                            (int)shipToReceiveOrders.Speed);
                        break;
                }

                int rowToAttack, columnToAttack;

                //ask for row and column to attack on map
                Console.WriteLine("Row to attack: ");
                rowToAttack = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Column to attack: ");
                columnToAttack = Convert.ToInt32(Console.ReadLine());

                RequestMissleTarget(rowToAttack, columnToAttack);
                //shipToReceiveOrders.PreTimeStep();
            }

        }

        public void PreTimeStep()
        {
            for (int i = 0; i < Navy.Capacity; i++)
            {
                Navy[i].PreTimeStep();
            }

        }

        public void TimeStep()
        {
            for (int i = 0; i < Navy.Capacity; i++)
            {
                Navy[i].TimeStep();
            }
        }

        public void PostTimeStep()
        {
            for (int i = 0; i < Navy.Capacity; i++)
            {
                Navy[i].PostTimeStep();
            }

            //increment turns since the attempted and successful attacks
            //and add to the successful and attempted attacks

        }

        public void RequestMissleTarget(int row, int column)
        {
            currentGame.Attack(this, row, column);
        }

        public void ReceiveMessage(string message)
        {
            Console.WriteLine(message);
        }

        public IGame currentGame;
    }
}
