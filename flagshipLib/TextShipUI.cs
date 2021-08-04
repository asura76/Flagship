using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    class TextShipUI : IShipUI
    {
        // This method should be called repeatedly by the Game class, once
        // for each ship in the game.
        //     Player is prompted with:
        //          The class of the ship, the Length in squares and # in the navy
        //     Player provides: Name, row, column, Direction, velocity.
        // After each placement, the user is shown a text-based rendering of
        // the map with the ships shown.

        // Resolve collisions
        public TextShipUI()
        {
            MapDrawer = new TextDrawMap();
            MapDrawer.DrawMap(MapRef);

        }

        //Player parameter for calling Player's recieveMessage function?
        public void placeShip(IShip shipToPlace, IPlayer player)
        {
            string shipNumber;
            int row = 0;
            int column = 0;
            Direction inputDirection = null;


            // Prompt for placing the ship by name and class.
            // Where do you want to place the U.S.S Titanic (an Aircraft Carrier length = 5)

            player.ReceiveMessage("Where would you like to place:" + "\n" +
                "The '" + shipToPlace.Name + "' a " + shipToPlace.Class + "of length "
                + shipToPlace.LengthInSquares + "\n" + "Row(range 0-" + MapRef.NRows + "): ");

            //Console.WriteLine("Where would you like to place:" + "\n" + 
            //    "The '" + shipToPlace.Name + "' a " + shipToPlace.Class + "of length "
            //    + shipToPlace.LengthInSquares + "\n" + "Row(range 0-" + MapRef.NRows + "): ");
            player.SetIntAnswer(row);
            while (row < MapRef.NRows)
            {
                if(row > MapRef.NRows ||
                    row < 0)
                {
                    player.ReceiveMessage("Out of range. Please put number between 0 and "
                        + MapRef.NRows + ": ");
                    player.SetIntAnswer(row);
                }
            }
            

            player.ReceiveMessage("Column (range 0-" + MapRef.NColumns + "): ");
            player.SetIntAnswer(column);
            while (column < MapRef.NRows)
            {
                if (column > MapRef.NColumns ||
                    column < 0)
                {
                    player.ReceiveMessage("Out of range. Please put number between 0 and "
                        + MapRef.NColumns + ": ");
                    player.SetIntAnswer(column);
                }
            }

            player.ReceiveMessage("Direction (E for east, W for West, etc.) ");
            shipToPlace.Direction.setDirectionByString(Console.ReadLine());

            MapRef.theMap[row, column] = null;
            MapRef.theMap[row, column].Add(shipToPlace);

            MapRef.resolveCollisions();

        }

        //
        public void takeTurn(IPlayer player, string shipName)
        {
            int nNavy = 0;
            string answer = " ";

            Console.WriteLine("Ship name you want to move: " );
            answer = Console.ReadLine();
            player.getShipByName(answer);
        }

        Player playersShips;

        public IDrawMap MapDrawer;
        public FlagshipMap MapRef;
    }
}
