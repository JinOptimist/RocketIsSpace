using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class InputViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Извини, но имя обязательно")]
        public string UserName { get; set; }
        public string Login { get; set; }

        [MaxLength(3)]
        public string Password { get; set; }

        [Min(18, ErrorMessage = "Только совершеннолетние")]
        public int Age { get; set; }

        [Min(0)]
        public int Money { get; set; }

        public string Bio { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
