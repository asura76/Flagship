using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    class Missile : IMapObject
    {
        public Missile(IMapObject thingToAttack, int row, int column)
        {
            Invincibility = true;
            CollideWith(thingToAttack, row, column);
        }
        public int Row { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Column { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Invincibility { get; private set; }
        public IMessageReceiver MessageReceiver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get { return "Missile"; } set { Name = value; } }

        public bool Attack(int row, int column)
        {
            return true;
        }

        public void CollideWith(IMapObject thingColliding, int row, int column)
        {
            thingColliding.Attack(row, column);
        }

        public bool isDestroyed(int row, int column)
        {
            return false;
        }

        public void PostTimeStep()
        {
            throw new NotImplementedException();
        }

        public void PreTimeStep()
        {
            throw new NotImplementedException();
        }

        public void TimeStep()
        {
            throw new NotImplementedException();
        }
    }
}
