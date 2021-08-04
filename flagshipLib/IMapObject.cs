using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public interface IMapObject
    {
        int Row { get; set; }
        int Column { get; set; }

        string Name { get; set; }

        // Move
        void PreTimeStep();

        // Determine the consequences of moving
        void TimeStep();

        // Resolve the consequences of moving
        void PostTimeStep();

        //   When object collide, make a call to the object's
        //   CollideWith function and pass it a copy of the object
        //   that it is colliding with.
        void CollideWith(IMapObject thingColliding, int row, int column);

        // Destroys the part ofthe MapObject at the specified
        // coordinates.
        /// <summary>
        ///  
        /// </summary>
        /// <param name=""></param>
        /// <returns>true if the attack is successful, false otherwise</returns>
        bool Attack(int row, int column);

        bool isDestroyed(int row, int column);

        bool Invincibility { get; }

        IMessageReceiver MessageReceiver { get; set; }
        // Make one kind of ship aircraft carrier.
        // Make a Missile class.
    }
}
