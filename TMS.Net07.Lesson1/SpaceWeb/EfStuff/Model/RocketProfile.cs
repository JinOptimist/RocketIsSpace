using System;
using HumansResources.Humans.Persons;
using SpaceWeb.Models.RocketModels;

namespace SpaceWeb.EfStuff.Model
{
    public class RocketProfile:BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; }

        public string PostAddress { get; }
        public string Email { get; set; }
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}