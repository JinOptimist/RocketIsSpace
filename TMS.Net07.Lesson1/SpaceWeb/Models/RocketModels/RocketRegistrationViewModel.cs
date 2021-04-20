using System;
using System.ComponentModel.DataAnnotations;
using HumansResources.Humans.Persons;

namespace SpaceWeb.Models.RocketModels
{
    public class RocketRegistrationViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}