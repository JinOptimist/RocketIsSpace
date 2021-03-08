using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.RocketFactory;

namespace Rocket.ComfortStructure
{
    public interface IComfortStructure:IRocket
    {
        double Weight { get; }
        string GetInfo();
    }
}
