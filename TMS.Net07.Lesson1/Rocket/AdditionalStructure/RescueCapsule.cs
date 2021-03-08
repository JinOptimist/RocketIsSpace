using System;

namespace Rocket.AdditionalStructure
{
    public class RescueCapsule : IAdditionalStructure
    {
        public double Weight { get; set; }
        public bool IsOpened { get; set; } = false;
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
    }
}