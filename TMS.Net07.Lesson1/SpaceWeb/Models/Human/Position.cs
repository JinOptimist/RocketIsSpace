using SpaceWeb.Localization;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models.Human
{
    public enum Position
    {
        [Display(ResourceType = typeof(Resource), Name = "Positin_Leader")]
        Leader = 1,
        [Display(ResourceType = typeof(Resource), Name = "Position_Deputy")]
        Deputy = 2,
        [Display(ResourceType = typeof(Resource), Name = "Position_Manager")]
        Manager = 3,
        [Display(ResourceType = typeof(Resource), Name = "Position_Trainee")]
        Trainee = 4,
        [Display(ResourceType = typeof(Resource), Name = "Position_ProfessionalWorker")]
        ProfessionalWorker = 5
    }
}