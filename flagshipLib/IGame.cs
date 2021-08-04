using System.Collections.Generic;

namespace FlagshipLib
{
    public interface IGame
    {
        List<IPlayer> Players { get; }
        IPlayer CurrentPlayer { get; set; }
        List<IPlayer> Winners { get; }
        bool GameIsOver { get; }

        void AddPlayer(IPlayer newPlayer);
        void Attack(IPlayer attackingPlayer, int row, int column);
        bool GameOver();
        void PlaceShip(IPlayer player, IShip ship);
        void FakeShipFactory(IPlayer player, string shipType);
        void playGame(int nPlayers);
        List<IShip> ShipList(IPlayer player);
        IPlayer Winner();
        FlagshipMap theMap { get; set; }
    }
}