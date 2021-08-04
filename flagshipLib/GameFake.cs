using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class GameFake : IGame
    {
        // x Make Fakes for all functions that utilize the console

        public IPlayer Player1;
        public IPlayer Player2;
        public IGame GameToTest;
        public IShip firstShip = new BattleShip(1, 2, defaultDirection, "S.S. Blitzkreig", firstID);
        public IShip secondShip = new Cruiser(5, 6, defaultDirection, "S.S. Comeraderie", secondID);
        public static Direction.CompassPoint defaultDirection = Direction.CompassPoint.East;
        public static int firstID = 1234;
        public static int secondID = 5678;

        public FlagshipMap MapRef;

        public List<IPlayer> Players => throw new NotImplementedException();

        public List<IPlayer> Winners => throw new NotImplementedException();

        public bool GameIsOver => throw new NotImplementedException();

        public IPlayer CurrentPlayer => throw new NotImplementedException();

        IPlayer IGame.CurrentPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FlagshipMap theMap { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameFake()
        {
            Player1 = new Player(GameToTest);
            Player2 = new Player(GameToTest);
            GameToTest = new Game();
        }
        
        public void TestShipFactory()
        {
            GameToTest.FakeShipFactory(Player1, "Cruiser");
        }

        public void TestPlayerPlaceShip()
        {
            IShip firstShip = new BattleShip(5, 6, defaultDirection, "S.S. Blitzkreig", firstID);

            Player1.PlaceShip(firstShip);
        }

        public void TestPlayer1Command(IPlayer player, string newDirection, int attackRow, int attackColumn)
        {
            player.FakeRequestCommand(player.Navy[0], newDirection, attackRow, attackColumn);
        }
        

        public void Player2Loses()
        {
            GameToTest.AddPlayer(Player1);
            GameToTest.AddPlayer(Player2);

            Player1.PlaceShip(firstShip);
            Player2.PlaceShip(secondShip);
            

            int nPlayers = GameToTest.Players.Capacity;

            Player1.FakeRequestCommand(firstShip, "East", 5, 6);
            Player1.FakeRequestCommand(firstShip, "East", 5, 7);

            foreach(IPlayer players in GameToTest.Players)
            {
                players.PreTimeStep();
                players.TimeStep();
                players.PostTimeStep();
            }

            //changes GameIsOver boolean variable to true
            GameToTest.GameOver();
        }

        public void AddPlayer(IPlayer newPlayer)
        {
            throw new NotImplementedException();
        }

        public void Attack(IPlayer attackingPlayer, int row, int column)
        {
            throw new NotImplementedException();
        }

        public bool GameOver()
        {
            throw new NotImplementedException();
        }

        public void PlaceShip(IPlayer player, IShip ship)
        {
            throw new NotImplementedException();
        }

        public void FakeShipFactory(IPlayer player, string shipType)
        {
            throw new NotImplementedException();
        }

        public void playGame(int nPlayers)
        {
            throw new NotImplementedException();
        }

        public List<IShip> ShipList(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public IPlayer Winner()
        {
            throw new NotImplementedException();
        }
    }
}
