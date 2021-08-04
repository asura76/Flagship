using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class Land : IMapObject
    {

        public Land(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public bool Attack(int row, int column)
        {
            return false;
        }

        public void CollideWith(IMapObject collidingObject, 
            int row, int column)
        {
            collidingObject.Attack(row, column);
        }


        bool invincible_;

       public bool Invincibility
        {
            get { return invincible_; }
            set { invincible_ = true; }
        }

        public IMessageReceiver MessageReceiver { get; set; } 
            = new NullMessageReceiver();
        public string Name { get { return "/"; } set { Name = value; } }

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
    }
}
