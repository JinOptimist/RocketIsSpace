using System;

namespace Rocket2.AdditionStructure
{
    public class BotanicalCenter : IAdditionStructure
    {
        public double Mass { get; }
        public bool IsOpen { get; private set; } = false;
        public void MakeClosed() => IsOpen = false;
        public void MakeOpened() => IsOpen = true;

        private int _numberOfPlantSlots;
        public int NumberOfPlantSlots
        {
            get => _numberOfPlantSlots;

            private set
            {
                if (value > 0)
                {
                    _numberOfPlantSlots = value;
                }
                else
                {
                    throw new Exception("Wrong number of plant slots. Expected: numberOfPlantSlots > 0");
                }
            }
        }

        public BotanicalCenter(double mass, int numberOfPlantSlots)
        {
            if (mass > 0)
            {
                Mass = mass;
            }
            else
            {
                throw new Exception("Wrong mass. Expected: mass > 0");
            }

            if (numberOfPlantSlots > 0)
            {
                _numberOfPlantSlots = numberOfPlantSlots;
            }
            else
            {
                throw new Exception("Wrong number of plant slots. Expected: numberOfPlantSlots > 0");
            }
        }

        public string GetInfo()
        {
            return $"Botanical center mass: {Mass} tons" +
                   $"{Environment.NewLine}The center has {NumberOfPlantSlots} plant slots" +
                   $"{Environment.NewLine}It is " + (IsOpen ? "open" : "close") + " now";
        }
    }
}