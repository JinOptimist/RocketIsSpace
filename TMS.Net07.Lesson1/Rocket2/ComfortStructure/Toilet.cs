using System;

namespace Rocket2.ComfortStructure
{
    public class Toilet : IComfortStructure
    {
        private bool _isOpen = true;

        public Toilet(double mass)
        {
            if (mass > 0)
            {
                Mass = mass;
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
            return $"Toilet mass: {Mass} tons" +
                   $"{Environment.NewLine}Toilet is " + (_isOpen ? "open" : "close") + " now";
        }
    }
}