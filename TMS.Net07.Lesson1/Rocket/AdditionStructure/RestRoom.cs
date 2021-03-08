using System;

namespace Rocket.AdditionStructure
{
    public class RestRoom : IAdditionStructure
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
                    throw new Exception("Amount of people cannot be a negative value.");
                }
            }
        }

        public string GetInfo()
        {
            return $"Rest room mass is {Mass} kg." +
                   $"{Environment.NewLine}The room is designed for {PeopleCapacity} people." +
                   $"{Environment.NewLine}It is " + (IsOpen ? "open" : "close") + " now.";
        }
    }
}