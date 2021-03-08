using System;

namespace Rocket.ComfortStructure
{
    public class Toilet : IComfortStructure
    {
        private bool _isOpen = true;

        public Toilet(double weight)
        {
            if (weight > 0)
            {
                Mass = weight;
            }
            else
            {
                throw new Exception("Wrong mass. Expected: mass > 0");
            }
        }

        public void OpenToilet() => _isOpen = true;
        public void CloseToilet() => _isOpen = false;

        public double Mass { get; }

        public string GetInfo()
        {
            return $"Toilet mass: {Mass}";
        }
    }
}