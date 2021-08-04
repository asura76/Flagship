using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public interface IGameModel
    {
        IPlayer firstPlayer { get; set; }
        IPlayer secondPlayer { get; set; }
        IPlayer thirdPlayer { get; set; }
        IPlayer fourthPlayer { get; set; }

        FlagshipMap FormMap { get; }
        IGame game { get; set; }
    }
}
