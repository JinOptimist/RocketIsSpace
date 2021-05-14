using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.EfStuff.Model
{
    public enum Specification
    {
        [Display(Name= "Unknown d")]
        Unknown = 0,
        [Display(Name = "Leader d")]
        Leader = 1,
        [Display(Name = "Spaceman d")]
        Spaceman = 2,
        [Display(Name = "Scientist d")]
        Scientist = 3,
        [Display(Name = "Еngineer d")]
        Еngineer = 4,
        [Display(Name = "Technicist d")]
        Technicist = 5,
        [Display(Name = "Other d")]
        Other = 6
    }
}