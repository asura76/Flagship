using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class Water : IMapObject
    {

        public Water(int row, int column)
        {
            Row = row;
            Column = column;
            invincible_ = true;
        }


        public int Row { get; set; }
        public int Column { get; set; }

        public bool Attack(int row, int column)
        {
            return false;
        }

        public void CollideWith(IMapObject thingColliding, int row, int column)
        {
        }

        public bool Invincibility
        {
            get { return invincible_; }
            set { invincible_ = true; }
        }

        bool invincible_;

        public bool isDestroyed(int row, int column) { return false; }

        public void PreTimeStep()
        {

        }

        public void TimeStep()
        {
        }

        public void PostTimeStep()
        {

        }

        public IMessageReceiver MessageReceiver { get; set; }

        public string Name { get { return "~"; } }

        string IMapObject.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
