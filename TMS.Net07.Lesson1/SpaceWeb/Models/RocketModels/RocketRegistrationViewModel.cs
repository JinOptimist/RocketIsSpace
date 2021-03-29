using System;
using System.ComponentModel.DataAnnotations;
using HumansResources.Humans.Persons;

namespace SpaceWeb.Models.RocketModels
{
    public class RocketRegistrationViewModel
    {
        [Required]
        public string Name { get; set; }

        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public Email Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}