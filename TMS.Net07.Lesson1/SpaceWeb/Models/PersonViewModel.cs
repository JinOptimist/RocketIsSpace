using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public class PersonViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string PostAddress { get; set; }
    }
}
