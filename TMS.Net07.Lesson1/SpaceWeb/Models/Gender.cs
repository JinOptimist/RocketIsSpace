using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public enum Gender
    {
        [Display(Name = "Неопределено")]
        None = 0,
        [Display(Name = "Мужчина")]
        Male = 1,
        [Display(Name = "Женщина")]
        Female = 2
    }
}