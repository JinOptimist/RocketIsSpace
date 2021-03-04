using System;

namespace HumansResources.Human
{
    interface IHuman
    {
        string FirstName { get; }
        string SecondName { get; }
        DateTime DateOfBirth { get; }
        bool IsMale { get; }
        
    }
}