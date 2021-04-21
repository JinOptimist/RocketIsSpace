using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class ProfileViewModel
    {
        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Извини, но имя обязательно")]
        public string UserName { get; set; }

        [MaxLength(3)]
        public string Password { get; set; }

        [Min(18, ErrorMessage = "Только совершеннолетние")]
        public int Age { get; set; }

        [Min(0)]
        public int Money { get; set; }

        public string Bio { get; set; }
        public DateTime DateRegistration { get; set; }

        public string AvatarUrl { get; set; }

        public List<BankAccountViewModel> MyAccounts { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}
