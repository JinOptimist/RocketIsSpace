using System;
using Rocket;
using Rocket.AdditionalStructure;

namespace Rocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var room1 = new ObservationDeck(54.3);
            Console.WriteLine($"Is this room open? {room1.IsOpened}." +
                              $"The weight of the module with this room is {room1.Weight}");
        }
    }
}