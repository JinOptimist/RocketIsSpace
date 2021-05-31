using SpaceWeb.Localization;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public enum Specification
    {
        [Display(ResourceType = typeof(Resource),Name = "Specification_Unknow")]
        Unknown = 0,
        [Display(ResourceType = typeof(Resource), Name = "Specification_Leader")]
        Leader = 1,
        [Display(ResourceType = typeof(Resource), Name = "Specification_Spacemen")]
        Spaceman = 2,
        [Display(ResourceType = typeof(Resource), Name = "Specification_Scientist")]
        Scientist = 3,
        [Display(ResourceType = typeof(Resource), Name = "Specification_Еngineer")]
        Еngineer = 4,
        [Display(ResourceType = typeof(Resource), Name = "Specification_Technicist")]
        Technicist = 5,
        [Display(ResourceType = typeof(Resource), Name = "Specification_Other")]
        Other = 6
    }
}