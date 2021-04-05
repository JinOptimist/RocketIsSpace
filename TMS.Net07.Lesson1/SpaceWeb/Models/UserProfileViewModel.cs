using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "Не указано имя пользователя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано фамилия пользователя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Недопустимая длина фамилии")]
        public string SurName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [MinAge(18, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        [Required]
        public string Sex { get; set; }

        public string IdentificationPassport { get; set; }

        [Required]
        [RegularExpression(@"^\+375\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Номер телефона должен иметь формат +375XX-xxx-xx-xx")]
        public string PhoneNumber { get; set; }

        public string PostAddress { get; set; }

        public string FullName { get; set; }
    }
}
