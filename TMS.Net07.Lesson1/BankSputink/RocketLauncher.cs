using System;
using System.Collections.Generic;
using System.Text;
using BankSputink.Rocket;
using System.Linq;

namespace BankSputink
{
    public class RocketLauncher
    {
        public RocketLauncher(List<ISputnik> sputniks)
        {
            Sputniks = sputniks;
        }

        public List<ISputnik> Sputniks { get; private set; }

        public bool IsAnySputnikInProcess()
        {
            return Sputniks
                .Any(x => x.IsReadyToLaunch);
        }
    }
}
