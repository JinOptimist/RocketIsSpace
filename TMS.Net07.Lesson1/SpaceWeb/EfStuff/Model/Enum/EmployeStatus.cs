using SpaceWeb.Localization;
using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models.Human
{
    public enum EmployeStatus
    {
        [Display(ResourceType = typeof(Resource), Name = "EmployeStatus_Request")]
        Request = 0,
        [Display(ResourceType = typeof(Resource), Name = "EmployeStatus_Denied")]
        Denied = 1,
        [Display(ResourceType = typeof(Resource), Name = "EmployeStatus_Accepted")]
        Accepted = 2,
        [Display(ResourceType = typeof(Resource), Name = "EmployeStatus_Fired")]
        Fired = 3
    }
}