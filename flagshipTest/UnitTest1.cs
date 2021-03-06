using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlagshipLib;
using System.Collections.Generic;
using Moq;

namespace flagshipTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        // Create a map
        public void createAMap()
        {
            int N_ROWS = 200;
            int N_COLUMNS = 100;
            Map myMap = new Map(N_ROWS, N_COLUMNS);
            Assert.AreEqual(N_ROWS, myMap.NRows);
            Assert.AreEqual(N_COLUMNS, myMap.NColumns);
        }

        [TestMethod]
        public void createWater()
        {
            IMapObject water = new Water(1, 2);
            Assert.AreEqual(1, water.Row);
            Assert.AreEqual(2, water.Column);
        }

        [TestMethod]
        public void createShip()
        {
            //row, column, and speed
            double speed = 1.0;
            Direction direct = new Direction(Direction.CompassPoint.East);
            Ship ship = new Ship(1, 2, speed, direct.Current, 1, "S.S. Ship", 0);
            Assert.AreEqual(1, ship.Row);
            Assert.AreEqual(2, ship.Column);
            Assert.AreEqual(1.0, ship.Speed);
        }

        [TestMethod]
        public void createSizedShip()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            FlagshipMap.setDimensions(N_ROWS, N_COLUMNS);

            double speed = 1.0;
            Direction direct = new Direction(Direction.CompassPoint.East);
            const int SHIP_ROW = 1;
            const int SHIP_COLUMN = 4;
            const int SHIP_LENGTH = 4;
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            Ship ship = new Ship(SHIP_ROW, SHIP_COLUMN, speed, direct.Current, 
                SHIP_LENGTH, FirstShipName, FirstShipID);

            Assert.AreEqual(SHIP_ROW, ship.Row);
            Assert.AreEqual(SHIP_COLUMN, ship.Column);

            for(int i=SHIP_COLUMN;i>0;i--)
            {
                Assert.IsFalse(ship[SHIP_ROW, i].Hit);
            }
        }

        [TestMethod]
        public void moveShip()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            FlagshipMap.setDimensions(N_ROWS, N_COLUMNS);

            double speed = 1.0;
            Direction direct = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            Ship ship = new Ship(1, 2, speed, direct.Current, 1, FirstShipName, FirstShipID);
            ship.PreTimeStep();
            Assert.AreEqual(3, ship.Column);
        }


        [TestMethod]
        public void circumnavigate()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            Map myMap = new Map(N_ROWS, N_COLUMNS);

            double speed = 1.0;
            Direction direct = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            Ship ship = new Ship(1, 2, speed, direct.Current, 2, FirstShipName, FirstShipID);
            for (int i = 0; i < N_ROWS * N_COLUMNS; i++)
            {
                ship.PreTimeStep();
            }

            Assert.AreEqual(1, ship.Row);
            Assert.AreEqual(2, ship.Column);
        }


        [TestMethod]
        public void circumnavigateByHalfSteps()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            Map myMap = new Map(N_ROWS, N_COLUMNS);

            double speed = 1.0;
            Direction direct = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;

            Ship ship = new Ship(1, 2, speed, direct.Current, 2, FirstShipName, FirstShipID);

            for (int i = 0; i < N_ROWS * N_COLUMNS * 2; i++)
            {
                ship.PreTimeStep();
            }

            Assert.AreEqual(1, ship.Row);
            Assert.AreEqual(2, ship.Column);
        }

        [TestMethod]
        public void changeDirection()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            Map myMap = new Map(N_ROWS, N_COLUMNS);

            double speed = 1.0;
            Direction direct = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            Ship ship = new Ship(1, 2, speed, direct.Current, 2, FirstShipName, FirstShipID);
            ship.changeDir(Direction.CompassPoint.West, 4);
            ship.PreTimeStep();
            Assert.AreEqual(ship.Direction.timeStepsToRequestedDirection, 3);
            for (int i = 0; i < 4; i++)
            {
                ship.PreTimeStep();
            }
            
            Assert.AreEqual(ship.Direction.timeStepsToRequestedDirection, 0);
            Assert.AreEqual(ship.Direction.Current, Direction.CompassPoint.West);
        }

        [TestMethod]
        public void changeDirectionMultiple()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            Map myMap = new Map(N_ROWS, N_COLUMNS);

            double speed = .5;
            Direction direct = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            Ship ship = new Ship(1, 2, speed, direct.Current, 2, FirstShipName, FirstShipID);
            ship.changeDir(Direction.CompassPoint.West, 4);
            ship.changeDir(Direction.CompassPoint.South, 2);
            for (int i = 0; i < 5; i++)
            {
                ship.PreTimeStep();
            }
            Assert.AreEqual(ship.Direction.Current, Direction.CompassPoint.West);
            for(int i = 0; i < 3; i++)
            {
                ship.PreTimeStep();
            }
            Assert.AreEqual(ship.Direction.Current, Direction.CompassPoint.South);
        }

        [TestMethod]
        public void hitShip()
        {
            Map myMap = FlagshipMap.GetInstance();

            double speed = .5;
            Direction direct_1 = new Direction(Direction.CompassPoint.East);
            Direction direct_2 = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            string SecondShipName = "Ship 2";
            int SecondShipID = 5678;
            Ship ship_1 = new Ship(1, 2, speed, direct_1.Current, 2,FirstShipName, FirstShipID);
            Ship ship_2 = new Ship(5, 6, speed, direct_2.Current, 2, SecondShipName, SecondShipID);
            ship_1.Attack(5, 5);
            Assert.AreEqual(0, ship_1.squaresPendingDestruction.Count);

            ship_1.Attack(1, 2);
            Assert.AreEqual(1, ship_1.squaresPendingDestruction.Count);

            //ship_1.PreTimeStep();
            //ship_2.PreTimeStep();

            //Assert.IsFalse(ship_1.Attack(8, 9));
            //ship_1.TimeStep();
            //ship_2.TimeStep();

            //Assert.AreEqual(ship_2.squaresPendingDestruction[0], ship_2[5, 5]);

            //ship_1.PostTimeStep();
            //ship_2.PostTimeStep();

            //Assert.IsTrue(ship_2[5, 5].Hit);
            //Assert.IsFalse(ship_2.destroyed_);
        }

        [TestMethod]
        public void destroyShip()
        {
            int N_ROWS = 20;
            int N_COLUMNS = 30;
            Map myMap = new Map(N_ROWS, N_COLUMNS);

            double speed = .5;
            Direction direct_1 = new Direction(Direction.CompassPoint.East);
            Direction direct_2 = new Direction(Direction.CompassPoint.East);
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            string SecondShipName = "Ship 2";
            int SecondShipID = 5678;
            Ship ship_1 = new Ship(1, 2, speed, direct_1.Current, 2, FirstShipName,FirstShipID);
            Ship ship_2 = new Ship(5, 6, speed, direct_2.Current, 2, SecondShipName, SecondShipID);

            ship_2.Attack(5, 5);
            ship_2.Attack(5, 6);
            Assert.AreEqual(2, ship_2.squaresPendingDestruction.Count);
            
            //ship_1.PreTimeStep();
            //ship_2.PreTimeStep();

            //ship_1.Attack(5, 6);
            //ship_1.Attack(5, 5);
            //ship_1.TimeStep();
            ship_2.TimeStep();

            //Assert.AreEqual(ship_2.squaresPendingDestruction[0], ship_2[5, 6]);
            //Assert.AreEqual(ship_2.squaresPendingDestruction[1], ship_2[5, 5]);

            //ship_1.PostTimeStep();
            ship_2.PostTimeStep();
            Assert.IsTrue(ship_2.destroyed_);
        }

        [TestMethod]
        public void createLand()
        {
            IMapObject land = new Land(1, 2);
            Assert.AreEqual(1, land.Row);
            Assert.AreEqual(2, land.Column);
        }

        [TestMethod]
        // Retrieve an item from the map
        public void retrieveFromMap()
        {
            int N_ROWS = 200;
            int N_COLUMNS = 100;
            Map myMap = new Map(N_ROWS, N_COLUMNS);
            // Get the object in the top-left corner
            List<IMapObject> mapObject = myMap[10, 12];
            // IMapObject mapObject = myMap[10, 12];
            Assert.AreEqual(10, mapObject[0].Row);
            Assert.AreEqual(12, mapObject[0].Column);
        }



        // Show that map locations are updated
        //  - put the ship at its new location
        //  - remove the ship from its old location
        //  - collide with anything inbetween
        //  -    
        [TestMethod]
        public void collideShips()
        {
            Map myMap = FlagshipMap.GetInstance();


            double speed = 1.0;
            string FirstShipName = "Ship 1";
            int FirstShipID = 1234;
            string SecondShipName = "Ship 2";
            int SecondShipID = 5678;
            Ship ship_1 = new Ship(10, 15, 0, Direction.CompassPoint.East, 2, FirstShipName, FirstShipID);
            Ship ship_2 = new Ship(10, 15, speed, Direction.CompassPoint.West, 2, SecondShipName, SecondShipID);


            //ship_2.TimeStep();
            Assert.AreEqual(15, ship_1.Column);
            Assert.AreEqual(10, ship_1.Row);
            Assert.AreEqual(15, ship_2.Column);
            Assert.AreEqual(10, ship_2.Row);

            Assert.AreEqual(3, myMap[10, 15].Count);
            ship_1.PreTimeStep();
            ship_2.PreTimeStep();

            //myMap.resolveCollisions();
            ship_1.TimeStep();
            ship_2.TimeStep();
            Assert.AreEqual(ship_1.squaresPendingDestruction[0], ship_1[0]);
            //Assert.AreEqual(ship_2.squaresPendingDestruction[0], ship_2[0]);

            ship_1.PostTimeStep();
            ship_2.PostTimeStep();
            Assert.IsTrue(ship_2[0].Hit);
            Assert.IsTrue(ship_1[0].Hit);

            //Assert.AreEqual(ship_2.destroyed_, true);
        }

        [TestMethod]
        public void collideBattleShips()
        {
            Map myMap = FlagshipMap.GetInstance();

            string FirstShipName = "BattleShip 1";
            int FirstShipID = 1234;
            string SecondShipName = "BattleShip 2";
            int SecondShipID = 5678;

            BattleShip bShip_1 = new BattleShip(10, 15, Direction.CompassPoint.East, FirstShipName, FirstShipID);
            BattleShip bShip_2 = new BattleShip(10, 15, Direction.CompassPoint.West, FirstShipName, FirstShipID);

            bShip_1.PreTimeStep();
            bShip_2.PreTimeStep();


            bShip_1.TimeStep();
            bShip_2.TimeStep();

            //myMap.resolveCollisions();

            bShip_1.PostTimeStep();
            bShip_2.PostTimeStep();
            Assert.IsTrue(bShip_2[0].Hit);
            Assert.IsTrue(bShip_1[0].Hit);
        }

        [TestMethod]
        public void collideAirships()
        {
            Map myMap = FlagshipMap.GetInstance();

            string FirstShipName = "AirCraftCarrier 1";
            int FirstShipID = 1234;
            string SecondShipName = "AirCraftCarrier 2";
            int SecondShipID = 5678;

            AircraftCarrier aShip_1 = new AircraftCarrier(10, 15, Direction.CompassPoint.East, FirstShipName, FirstShipID);
            AircraftCarrier aShip_2 = new AircraftCarrier(10, 15, Direction.CompassPoint.West, FirstShipName, FirstShipID);

            aShip_1.PreTimeStep();
            aShip_2.PreTimeStep();


            aShip_1.TimeStep();
            aShip_2.TimeStep();

            //myMap.resolveCollisions();

            aShip_1.PostTimeStep();
            aShip_2.PostTimeStep();
            Assert.IsTrue(aShip_2[0].Hit);
            Assert.IsTrue(aShip_1[0].Hit);
        }

        [TestMethod]
        public void collideCruiser()
        {

            Map myMap = FlagshipMap.GetInstance();

            string FirstShipName = "Cruiser 1";
            int FirstShipID = 1234;
            string SecondShipName = "Cruiser 2";
            int SecondShipID = 5678;

            Cruiser cShip_1 = new Cruiser(10, 15, Direction.CompassPoint.East, FirstShipName, FirstShipID);
            Cruiser cShip_2 = new Cruiser(10, 15, Direction.CompassPoint.West, SecondShipName, SecondShipID);

            cShip_1.PreTimeStep();
            cShip_2.PreTimeStep();


            cShip_1.TimeStep();
            cShip_2.TimeStep();

            //myMap.resolveCollisions();

            cShip_1.PostTimeStep();
            cShip_2.PostTimeStep();
            Assert.IsTrue(cShip_2[0].Hit);
            Assert.IsTrue(cShip_1[0].Hit);
        }

        [TestMethod]
        public void containsTest()
        {

            string FirstShipName = "Cruiser 1";
            int FirstShipID = 1234;

            Cruiser cShip_1 = new Cruiser(10, 15, Direction.CompassPoint.East, FirstShipName, FirstShipID);
            Assert.IsTrue(cShip_1.Contains(10, 15));
            Assert.IsTrue(cShip_1.Contains(10, 14));
            Assert.IsFalse(cShip_1.Contains(10, 16));
            Assert.IsFalse(cShip_1.Contains(10, 13));
            Assert.IsFalse(cShip_1.Contains(9, 15));
        }

        //[TestMethod]
        //public void gamePlaceShip()
        //{
        //    Map myMap = FlagshipMap.GetInstance();

        //    Game gameTest = new Game();

        //    int PlayerRow = 1, PlayerColumn = 5;

        //    Player player1 = new Player();

        //    string FirstShipName = "AirCraft Carrier 1";
        //    int FirstShipID = 1234;

        //    AircraftCarrier aircraft =
        //        new AircraftCarrier(PlayerRow, PlayerColumn, Direction.CompassPoint.East, FirstShipName, FirstShipID);

        //    gameTest.AddPlayer(player1);
        //    gameTest.Players[player1.].PlaceShip(player1.Navy[aircraft.Name]);
        //    //Checks for both the type and the item in player's Navy
        //    Assert.IsTrue(myMap[1, 5].Contains(aircraft));
        //    Assert.IsTrue(myMap[1, 5].Contains(player1.Navy[0]));
        //}

        //[TestMethod]
        //public void MultiplePlayersInGame()
        //{
        //    Map myMap = FlagshipMap.GetInstance();

        //    Game gameTest = new Game();

        //    int FirstPlayerRow = 1, FirstPlayerColumn = 5,
        //        SecondPlayerRow = 2, SecondPlayerColumn = 10;

        //    Player FirstPlayer = new Player();
        //    Player SecondPlayer = new Player();

            

        //}

        [TestMethod]
        public void TargetMissile()
        {
            Map myMap = FlagshipMap.GetInstance();

            Game gameTest = new Game();

            int PlayerRow = 1, PlayerColumn = 5,
                SecondPlayerRow = 2, SecondPlayerColumn = 10;

            Player FirstPlayer = new Player(gameTest);
            Player SecondPlayer = new Player(gameTest);

            string FirstShipName = "AirCraft Carrier 1";
            int FirstShipID = 1234;
            string SecondShipName = "BattleShip 1";
            int SecondShipID = 5678;

            AircraftCarrier FPAircraft =
                new AircraftCarrier(PlayerRow, PlayerColumn, Direction.CompassPoint.East,
                FirstShipName, FirstShipID);

            BattleShip SPBattleship =
                new BattleShip(SecondPlayerRow, SecondPlayerColumn, Direction.CompassPoint.West,
                SecondShipName, SecondShipID);

            gameTest.AddPlayer(FirstPlayer);
            gameTest.PlaceShip(FirstPlayer, FPAircraft);
            gameTest.AddPlayer(SecondPlayer);
            gameTest.PlaceShip(SecondPlayer, SPBattleship);

            FirstPlayer.RequestMissleTarget(2, 10);
            Assert.IsTrue(SPBattleship.squaresPendingDestruction.Count == 1);

        }

        //[TestMethod]
        //public void PlayerRequest()
        //{
        //    Map myMap = FlagshipMap.GetInstance();

        //    IGame gameTest = new Game();

        //    int PlayerRow = 1, PlayerColumn = 5;

        //    IPlayer FirstPlayer = new Player(gameTest);

        //    string FirstShipName = "Ship 1";
        //    int FirstShipID = 1234;

        //    IShip firstAircraftCarrier =
        //        new AircraftCarrier(PlayerRow, PlayerColumn, Direction.CompassPoint.East,
        //        FirstShipName, FirstShipID);

        //    gameTest.AddPlayer(FirstPlayer);
        //    gameTest.PlaceShip(FirstPlayer, firstAircraftCarrier);

        //    FirstPlayer.RequestCommand(firstAircraftCarrier);
        //}

        [TestMethod]
        public void DrawTheMap()
        {
            Map myMap = FlagshipMap.GetInstance();

            GameFake GameTest = new GameFake();

            IDrawMap DrawnMap = new TextDrawMap();
            //DrawnMap.DrawMap(myMap);

        }

        [TestMethod]
        public void PlayerPlaceShip()
        {
            Map myMap = FlagshipMap.GetInstance();
            GameFake Gametest = new GameFake();
            
            //Places a ship on the map without a Console
            Gametest.TestPlayerPlaceShip();

            Assert.AreEqual(myMap[5,6], Gametest.firstShip);
        }

        [TestMethod]
        public void PlayerRequestCommand()
        {
            Map myMap = FlagshipMap.GetInstance();
            GameFake Gametest = new GameFake();
            string newDirection = "South";

            //Tests the request for a new direction and attacks a spot on the map
            Gametest.TestPlayer1Command(Gametest.Player1, newDirection,Gametest.secondShip.Row, Gametest.secondShip.Column);

            Assert.AreEqual(Gametest.secondShip[5, 6].Hit, true);
        }

        [TestMethod]
        public void FactoryForShip()
        {
            Map myMap = FlagshipMap.GetInstance();
            GameFake Gametest = new GameFake();

            //Creates a Cruiser and adds it to the 
            //player's navy
            Gametest.TestShipFactory();

            Assert.AreEqual(Gametest.Player1.Navy[0].Class, "Cruiser");
        }

        [TestMethod]
        public void Player2LoseGame()
        {
            Map myMap = FlagshipMap.GetInstance();
            GameFake Gametest = new GameFake();

            Gametest.Player2Loses();

            Assert.AreEqual(Gametest.GameToTest.GameIsOver, true);

        }

        //[TestMethod]
        //public void TestGame()
        //{
            
        //}



    }
}
