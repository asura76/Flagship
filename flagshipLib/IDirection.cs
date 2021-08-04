namespace FlagshipLib
{
    public interface IDirection
    {
        Direction.CompassPoint Current { get; set; }

        void RequestChange(Direction.CompassPoint newDirection, int timeStepsRequired);
        void TimeStep();
    }
}