using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public class PersonViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }

        [MinAge(18)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Email { get; set; }

        [MyPhone]
        public string PhoneNumber { get; set; }

        public string PostAddress { get; set; }
    }
}