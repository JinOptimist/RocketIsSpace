using System;

namespace Rocket.AdditionStructure
{
    public class RescueCapsule : IAdditionStructure
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

        public RescueCapsule(double mass, int peopleCapacity)
        {
            if (mass > 0)
            {
                Mass = mass;
            }
            else
            {
                throw new Exception("Wrong mass. Expected: mass > 0");
            }

            if (peopleCapacity > 0)
            {
                PeopleCapacity = peopleCapacity;
            }
            else
            {
                throw new Exception("Wrong people capacity. Expected: peopleCapacity > 0");
            }
        }

        public string GetInfo()
        {
            return $"Rescue capsule mass: {Mass} tons" +
                   $"{Environment.NewLine}The capsule is designed for {PeopleCapacity} people" +
                   $"{Environment.NewLine}It is " + (IsOpen ? "open" : "close") + " now";
        }
    }
}