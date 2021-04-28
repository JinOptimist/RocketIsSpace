using System;
using SpaceWeb.Migrations;

namespace SpaceWeb.Models.RocketModels
{
    public class RocketProfileViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
        
    }
}