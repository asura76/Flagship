using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlagshipLib
{
    public class Ship : IShip, IEnumerable<ShipSquare>
    {
        public const string UnknownShip = "Unknown Ship";
        public const int UnknownNavy = 0;
        private static int nextShipId = 1;

        public static int NextShipId()
        {
            return nextShipId++;
        }

        public Ship(string name = "Unknown", int navyId=0)
        {

        }

        public Ship(int row, int column, double speed,
            Direction.CompassPoint direction,
            int length, string name = "Unknown", int navyId=0)
        {
            Name = name;
            NavyId = navyId;
            dRow = row;
            dColumn = column;
            Speed = speed;
            LengthInSquares = length;
            Direction = new Direction(direction);
            shipsquares_ = new ShipSquare[length];
            mapRef = FlagshipMap.GetInstance();
            for (int i = 0; i < length; i++)
            {
                shipsquares_[i] = new ShipSquare();
                shipsquares_[i].SquaresShip = this;
                shipsquares_[i].OffsetFromFront = i;
                shipsquares_[i].Hit = false;
                this.mapRef.theMap[shipsquares_[i].Row, shipsquares_[i].Column].Add(this);
            }
        }

        public bool Contains(int row, int column)
        {
            mapRef = FlagshipMap.GetInstance();
            bool contains = false;
            foreach (IMapObject ship in mapRef[row, column])
            {
                if (ship == (IMapObject)this) contains = true;
            }
            return contains;
        }


        public void changeDir(Direction.CompassPoint direction, int nSteps)
        {
            Direction.RequestChange(direction, nSteps);
        }

        public void changeDirByString(string direction, int nSteps)
        {
            Direction.requestByString(direction, nSteps);
        }

        public int Row
        {
            get
            {
                return (int)dRow;
            }
            set
            {
                dRow = value;
            }
        }

        public int Column
        {
            get
            {
                return (int)dColumn;
            }
            set
            {
                dColumn = value;
            }
        }


        private double dRow;
        private double dColumn;

        //dRow and dColumn represent the middle of the ship
        //
        public int LengthInSquares { get; private set; }

        //private double Width;


        public void PreTimeStep()
        {
            // Remove all map references
            foreach (ShipSquare square in this)
            {
                mapRef[square.Row, square.Column].Remove(this);
            }

            // Move the ship
            if (Direction.Current == Direction.CompassPoint.West)
                dColumn = (dColumn + Speed * -1);

            if (Direction.Current == Direction.CompassPoint.East)
                dColumn = (dColumn + Speed);

            if (Direction.Current == Direction.CompassPoint.North)
                dRow = (dRow + Speed * -1);

            if (Direction.Current == Direction.CompassPoint.South)
                dRow = (dRow + Speed);

            //// See if we are still on the Map
            //FlagshipMap map = FlagshipMap.GetInstance();
            //if (dRow > map.NRow)
            if (dRow >= mapRef.NRows) { dRow = 0; }
            if (dColumn >= mapRef.NColumns) { dColumn = 0; }
            if (dRow < 0) { dRow += mapRef.NRows; }
            if (dColumn < 0) { dColumn += mapRef.NColumns; }

            Direction.TimeStep();

            foreach (ShipSquare square in this)
            {
                mapRef[square.Row, square.Column].Add(this);
            }
            //// Make all map references
        }

        public virtual void TimeStep()
        {
            //11/23
            //Is currently making it so that each part of ship is hit
            //need to make so that only part of ship hit is marked
            foreach (ShipSquare square in shipsquares_)
            {
                foreach (IMapObject mapObject in mapRef[square.Row, square.Column])
                {
                    CollideWith(mapObject, square.Row, square.Column);
                }
            }
        }

        public virtual void PostTimeStep()
        {
            foreach (ShipSquare square in squaresPendingDestruction)
            {
                square.Hit = true;
            }

            IsShipDestroyed();

            //isDestroyed(Row, Column);
        }

        public void CollideWith(IMapObject thingColliding, int row, int column)
        {
            if (thingColliding is IShip)
            {
                IShip theShip = (IShip)thingColliding;
                if (theShip.Contains(row, column))
                {
                    theShip[row, column].SquaresShip.Attack(row, column);
                    thingColliding.Attack(row, column);
                }
            }
        }

        //"this" should reference the front and shipSquares
        public bool Attack(int row, int column)
        {
            // Find the ship square at (row, column)
            bool squareDestroyed = false;

            if (Contains(row, column))
            {
                ShipSquare attackedSquare = this[row, column];
                if (attackedSquare != null)
                {
                    squaresPendingDestruction.Add(attackedSquare);
                    squareDestroyed = true;
                }
            }
            return squareDestroyed;
        }

        public ShipSquare this[int offsetFromFront]
        {
            get
            {
                return shipsquares_[offsetFromFront];
            }
        }

        public ShipSquare this[int row, int column]
        {
            get
            {
                int squareOffset = 0;
                if ((Row != row) || (Column != column))
                {
                    if (row == Row)
                    {
                        squareOffset = Math.Abs(column - Column);
                    }
                    else
                    {
                        squareOffset = Math.Abs(row - Row);
                    }
                }
                return shipsquares_[squareOffset];
            }
        }

        public IEnumerator<ShipSquare> GetEnumerator()
        {
            return ((IEnumerable<ShipSquare>)shipsquares_).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ShipSquare>)shipsquares_).GetEnumerator();
        }

        public bool Invincibility
        {
            get { return invincible_; }
            set { invincible_ = false; }
        }

        public bool Destroyed { get; private set; }

        //parameters are for attack/collideWith functions
        public bool isDestroyed(int row, int column)
        {
            if (this[row, column].Hit == false)
            {
                this[row, column].Hit = true;
            }

            for (int i = 0; i < this.shipsquares_.Length; i++)
            {
                if (shipsquares_[i].Hit != true)
                {
                    destroyed_ = false;
                }
            }

            destroyed_ = true;
            return destroyed_;
        }

        public bool IsShipDestroyed()
        {
            int nSquaresDestroyed = 0;
            foreach (ShipSquare square in this.shipsquares_)
            {
                if (square.Hit == true)
                    ++nSquaresDestroyed;
            }

            if(nSquaresDestroyed == shipsquares_.Length)
            {
                destroyed_ = true;
            }

            return destroyed_;
        }

        public IMessageReceiver MessageReceiver { get; set; }

        public Direction Direction { get; private set; }

        public double Speed { get; private set; }

        public string Name { get; set; }

        public string Class { get; set; }


        public int NavyId { get; set; }
        public Bitmap Image { get; protected set; }

        public ShipSquare[] shipsquares_ { get; set; }
        public int NTurnsTilMove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FlagshipMap mapRef;

        bool invincible_ = false;

        public List<ShipSquare> squaresPendingDestruction { get; set; } = new List<ShipSquare>();

        public bool destroyed_ { get; set; } = false;

        public int[] ShipHeading = new int[2];
    }
}


