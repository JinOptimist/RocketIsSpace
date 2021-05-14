using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public enum DepartmentType
    {
        [Display(Name= "Unknown disply")]
        Unknown = 0,
        [Display(Name = "Manufactory disply")]
        Manufactory = 1,
        [Display(Name = "Laboratory disply")]
        Laboratory = 2,
        [Display(Name = "MissionControlCenter disply")]
        MissionControlCenter = 3,
        [Display(Name = "SpacecraftCrew disply")]
        SpacecraftCrew = 4,
        [Display(Name = "Other disply")]
        Other = 5
    };
}