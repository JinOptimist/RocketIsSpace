using System;

namespace Rocket.AdditionStructure
{
    public class ObservationDeck : IAdditionStructure
    {
        public double Mass { get; set; }
        public bool IsOpen { get; private set; } = false;
        public void MakeClosed() => IsOpen = false;
        public void MakeOpened() => IsOpen = true;

        private int _peopleCapacity;

        public int PeopleCapacity
        {
            get => _peopleCapacity;

            private set
            {
                if (value > 0)
                {
                    _peopleCapacity = value;
                }
                else
                {
                    throw new Exception("Wrong amount. Expected: amount > 0");
                }
            }
        }

        public string GetInfo()
        {
            return $"Observation deck mass: {Mass} tons" +
                   $"{Environment.NewLine}The deck is designed for {PeopleCapacity} people" +
                   $"{Environment.NewLine}It is " + (IsOpen ? "open" : "close") + " now";
        }
    }
}