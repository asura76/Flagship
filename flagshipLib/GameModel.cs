using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class GameModel : IGameModel
    {
        public IPlayer firstPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPlayer secondPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPlayer thirdPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPlayer fourthPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FlagshipMap FormMap
        {
            get
            {
                return FlagshipMap.GetInstance();
            }
        }

        public IGame game { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
