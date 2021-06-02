using SpaceWeb.Localization;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models.Human
{
    public enum DepartmentType
    {
        [Display(ResourceType = typeof(Resource), Name = "DepartmentType_Unknown")]
        Unknown = 0,
        [Display(ResourceType = typeof(Resource), Name = "DepartmentType_Manufactory")]
        Manufactory = 1,
        [Display(ResourceType = typeof(Resource), Name = "DepartmentType_Laboratory")]
        Laboratory = 2,
        [Display(ResourceType = typeof(Resource), Name = "DepartmentType_MissionControlCenter")]
        MissionControlCenter = 3,
        [Display(ResourceType = typeof(Resource), Name = "DepartmentType_SpacecraftCrew")]
        SpacecraftCrew = 4,
        [Display(ResourceType = typeof(Resource), Name = "DepartmentType_Other")]
        Other = 5
    };
}