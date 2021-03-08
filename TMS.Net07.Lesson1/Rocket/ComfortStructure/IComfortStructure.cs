using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.ComfortStructure
{
    public interface IComfortStructure
    {
        double Weight { get; }
        string GetInfo();
    }
}
