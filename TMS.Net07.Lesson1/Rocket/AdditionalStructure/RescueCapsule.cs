using System;

namespace Rocket.AdditionalStructure
{
    public class RescueCapsule : IAdditionalStructure
    {
        public double Weight { get; set; }
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
            return $"Rescue capsule weight is {Weight} kg." +
                   $"{Environment.NewLine}The capsule is designed for {PeopleCapacity} people." +
                   $"{Environment.NewLine}It is " + (IsOpen ? "open" : "close") + " now.";
        }
    }
}