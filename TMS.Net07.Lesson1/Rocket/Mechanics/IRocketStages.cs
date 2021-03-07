using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Mechanics
{
    public interface IRocketStages
    {
        public double StageWeight { get; set; }
        public double StageFuelConsumption { get; set; }
        public double StageFuelTanksCapacity { get; set; }
        string StageDetaching();
    }
}