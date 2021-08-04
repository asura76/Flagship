using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class Direction : IDirection
    {
        public enum CompassPoint { North, South, East, West, def };

        public Direction(CompassPoint point)
        {
            Current = point;
        }

        public void setDirectionByString(string directionToSet)
        {
            //CompassPoint directToSet = new CompassPoint();
            switch (directionToSet)
            {
                case "East":
                    Current = CompassPoint.East;
                    break;

                case "West":
                    Current = CompassPoint.West;
                    break;

                case "North":
                    Current = CompassPoint.North;
                    break;

                case "South":
                    Current = CompassPoint.South;
                    break;
            }
        }

        public Direction.CompassPoint directionByString(string directionToSet)
        {
            Direction.CompassPoint stringToDirection = CompassPoint.def;
            switch (directionToSet)
            {
                case "East":
                    stringToDirection = CompassPoint.East;
                    break;

                case "West":
                    stringToDirection = CompassPoint.West;
                    break;

                case "North":
                    stringToDirection = CompassPoint.North;
                    break;

                case "South":
                    stringToDirection = CompassPoint.South;
                    break;
            }

            return stringToDirection;
        }

        public void requestByString(string directionToSet, int timeStepRequired)
        {
            switch (directionToSet)
            {
                case "East":
                    RequestChange(directionByString(directionToSet), timeStepRequired);
                    break;

                case "West":
                    RequestChange(directionByString(directionToSet),timeStepRequired);
                    break;

                case "North":
                    RequestChange(directionByString(directionToSet), timeStepRequired);
                    break;

                case "South":
                    RequestChange(directionByString(directionToSet), timeStepRequired);
                    break;
            }
        }

        public void RequestChange(CompassPoint newDirection,
            int timeStepsRequired)
        {
            if (timeStepsToRequestedDirection == 0)
            {
                timeStepsToRequestedDirection = timeStepsRequired;
                requestedChange = newDirection;
            }
            else
            {
                requestedChangeQueue.Enqueue(
                    new Tuple<CompassPoint, int>(newDirection,
                    timeStepsRequired));
            }
        }

        public static void computeRowColumnIncrements(
            CompassPoint compassPoint,
            out int rowInc, 
            out int columnInc)
        {
            rowInc = 0;
            columnInc = 0;
            switch (compassPoint)
            {
                case Direction.CompassPoint.East:
                    columnInc = -1;
                    break;

                case Direction.CompassPoint.West:
                    columnInc = 1;
                    break;

                case Direction.CompassPoint.North:
                    rowInc = -1;
                    break;

                case Direction.CompassPoint.South:
                    rowInc = 1;
                    break;
            }
        }

        public void TimeStep()
        {
            if (timeStepsToRequestedDirection > 0)
            {
                --timeStepsToRequestedDirection;
                if (timeStepsToRequestedDirection == 0)
                {
                    Current = requestedChange;
                    if (requestedChangeQueue.Count > 0)
                    {
                        Tuple<CompassPoint, int> tuple =
                            requestedChangeQueue.Dequeue();
                        requestedChange = tuple.Item1;
                        timeStepsToRequestedDirection = tuple.Item2;
                    }
                }
            }
        }

        public CompassPoint Current { get; set; }
        CompassPoint requestedChange = 0;

        Queue<Tuple<CompassPoint, int>> requestedChangeQueue =
                new Queue<Tuple<CompassPoint, int>>();
        public int timeStepsToRequestedDirection;
    }
}
