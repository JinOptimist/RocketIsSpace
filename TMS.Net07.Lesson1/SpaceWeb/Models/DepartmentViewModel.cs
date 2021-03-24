using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public enum DepartmentType
    {
        Unknown = 0,
        Manufactory = 1,
        Laboratory = 2,
        MissionControlCenter = 3,
        SpacecraftCrew = 4,
        Other = 5
    };
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "Department name is required!")]
        public string DepartmentName { get; set; }
        public DepartmentType DepartmentType { get; set; }
        public int MaximumCountEmployes { get; set; } = 1;
        public int HourStartWorking { get; set; } = 9;
        public int HourEndWorking { get; set; } = 17;
    }
}
