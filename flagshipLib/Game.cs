using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// ToDo
//   Finish Game class to include things like
//      SendMessages to players
//      x Determine if the game is over
//      x Advance the timestep
//      Finish the place ship functions.
//      Write draw function for map to generate text-based map.
//S
//  WPF
//    Finish the menu that lets us change the map size
//    Provide a UI to place a ship
//    Provide the UI to draw the ship

namespace FlagshipLib
{
    public class Game : IGame
    {
        public const int DEFAULT_N_ROWS = 20;
        public const int DEFAULT_N_COLUMNS = 20;
        public Game(int nRows = DEFAULT_N_ROWS, int nColumns = DEFAULT_N_COLUMNS)
        {
            Players = new List<IPlayer>();
            Winners = new List<IPlayer>();
            textMap = new TextDrawMap();
            textCommands = new TextShipUI();

            FlagshipMap.setDimensions(nRows, nColumns);
            theMap = FlagshipMap.GetInstance();
            GameIsOver = false;
        }



        //public void PlaceShip(IPlayer player, IShip ship)
        //{
        //    int rowInc;
        //    int columnInc;

        //    Direction.computeRowColumnIncrements(ship.Direction.Current,
        //        out rowInc, out columnInc);

        //    int row = ship.Row;
        //    int column = ship.Column;

        //    // Copy the ship to the map
        //    for (int i = 0; i < ship.LengthInSquares; i++)
        //    {
        //        theMap[row, column].Add(ship);
        //        row += rowInc;
        //        column += columnInc;
        //    }

        //    // player.PlaceShip(ship);

        //}

        public void Attack(IPlayer attackingPlayer, int row, int column)
        {
            foreach (IMapObject mapObject in theMap[row, column])
            {
                mapObject.Attack(row, column);
            }
        }

        //public void SetMapSize(int newRows, int newColumns)
        //{
        //    theMap = new Map(newRows, newColumns);
        //}

        public IPlayer Winner()
        {
            return Winners[0]; 
        }

        public bool GameOver()
        {

            bool isOver = false;

            foreach (IPlayer player in Players)
            {
                if (player.NavyDestroyed() != true)
                {
                    Winners.Add(player);
                }
            }

            if (Winners.Count >= 2)
            {
                Winners = new List<IPlayer>();
            }
            else
            {
                isOver = true;
                GameIsOver = true;
            }

            return isOver;
        }

        public List<IShip> ShipList(IPlayer player)
        {
            return player.Navy;
        }

        public void FakeShipFactory(IPlayer player, string shipType)
        {
            Ship newShip = null;
            switch (shipType)
            {
                case "AircraftCarrier":
                    newShip = new AircraftCarrier(0, 0, Direction.CompassPoint.North);
                    break;

                case "Battleship":
                    newShip = new BattleShip(0, 0, Direction.CompassPoint.North);
                    break;

                case "Cruiser":
                    newShip = new Cruiser(0, 0, Direction.CompassPoint.North);
                    break;
            }

            player.Navy.Add(newShip);
        }

        //
        private Ship ShipFactory(string shipType)
        {
            Ship newShip = null;
            switch (shipType)
            {
                case "AircraftCarrier":
                    newShip = new AircraftCarrier(0, 0, Direction.CompassPoint.North);
                    break;

                case "Battleship":
                    newShip = new BattleShip(0, 0, Direction.CompassPoint.North);
                    break;

                case "Cruiser":
                    newShip = new Cruiser(0, 0, Direction.CompassPoint.North);
                    break;
            }

            return newShip;
        }

        public void FakeGameSim(int nPlayers)
        {
            string[] gameShips = { "AircraftCarrier",
                "AircraftCarrier", "Battleship"};

            foreach (IPlayer player in Players)
            {
                foreach (string shipType in gameShips)
                {
                    FakeShipFactory(player, shipType);
                    while(GameOver() != false)
                    {
                        
                    }
                }
            }
        }
        

        public void GameSetUp(int nPlayers, int nShips)
        {
            for(int i = 0; i < nPlayers; i++)
            {
                commandQuestion = "Which kind of ship";
                // gameForm.Text = commandQuestion;
            }
        }

        //should ask if player
        public void playGame(int nPlayers)
        {
            string pName, pID, answer;
            IPlayer newPlayer;

            ////should ask each player if want to load info from file
            //Console.WriteLine("Load player data from file? Y/N");
            //answer = Console.ReadLine();
            //if(answer.ToUpper() == "Y")
            //{
            //    Console.WriteLine("File name: ");
            //    answer = Console.ReadLine();
            //    PlayerIO.Load(answer, this);

            //    //condition for if file is not found
            //}

            // if less than 2 players on file, will ask again
            // and if no, ask if want to make new player


            
            string[] gameShips = { "AircraftCarrier",
                "AircraftCarrier", "Battleship"};

            ////regular player information loading
            ////auto loads one of each type of ship
            //for (int i = 1; i <= nPlayers; i++)
            //{
            //    Console.WriteLine("Player "+ i + " Name: ");
            //    pName = Console.ReadLine();
            //    Console.WriteLine("Player " + i + "Navy ID");
            //    pID = Console.ReadLine();
            //    newPlayer = new Player(this, pName, pID);
            //    AddPlayer(newPlayer);
            //}

            // For all players place all ships
            foreach (IPlayer player in Players)
            {
                foreach (string shipType in gameShips)
                {
                    Ship newShip = ShipFactory(shipType);
                    player.Navy.Add(newShip);
                    textCommands.placeShip(newShip, player);
                    //player.PlaceShip(newShip);
                    //PlaceShip(player, newShip);
                    
                    // Record the ship for this player
                }
            }
            

            // While the game isn't over
            while (GameIsOver == false)
            {
                textMap.DrawMap(theMap);
                // Advance time
                

                // Perform time increment
                foreach (IPlayer player in Players)
                {
                    foreach (Ship ship in ShipList(player))
                    {
                        ship.PreTimeStep();
                        player.RequestCommand(ship);
                    }

                    const int nMisslesPerMove = 3;
                    int row = 0;
                    int column = 0;
                    for(int i=0;i<nMisslesPerMove;i++)
                    {
                        player.RequestMissleTarget(row, column);
                        //Attack(player, row, column);

                        //Do timestep within Attack function?
                        foreach (Ship ship in ShipList(player))
                        {
                            ship.TimeStep();
                        }

                        // Player gets a status of the attacke
                        // player.ReceiveMessage()
                    }

                    
                }
            }
        }



        // Get thePlayers
        public List<IPlayer> Players { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public FlagshipMap theMap { get; set; }
        public List<IPlayer> Winners { get; private set; }
        public bool GameIsOver { get; private set; }
        public IDrawMap textMap { get; private set; }
        public IShipUI textCommands { get; private set; }
        public IGameIO PlayerIO { get; private set; }
        public string commandQuestion;
        public void AddPlayer(IPlayer newPlayer)
        {
            Players.Add(newPlayer);
        }

        public void PlaceShip(IPlayer player, IShip ship)
        {
        }

        //public void setPlayerShips()
        //{
        //    for (int i = 0; i < Players.Count; i++)
        //    {

        //        switch (i)
        //        {
        //            case 0:
        //                foreach (var ship in Players[i].Navy)
        //                {
        //                    ship.Direction.Current = Direction.CompassPoint.East;
        //                    PlaceShip(Players[i], ship);
        //                }
        //                break;
        //            case 1:
        //                foreach (var ship in Players[i].Navy)
        //                {
        //                    ship.Direction.Current = Direction.CompassPoint.West;
        //                    PlaceShip(Players[i], ship);
        //                }
        //                break;
        //            case 2:
        //                foreach (var ship in Players[i].Navy)
        //                {
        //                    ship.Direction.Current = Direction.CompassPoint.South;
        //                    PlaceShip(Players[i], ship);
        //                }

        //                break;
        //            case 3:
        //                foreach (var ship in Players[i].Navy)
        //                {
        //                    ship.Direction.Current = Direction.CompassPoint.North;
        //                    PlaceShip(Players[i], ship);
        //                }

        //                break;
        //        }
        //    }
        //}


    }
}
