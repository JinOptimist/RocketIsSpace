using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class Department : BaseModel
    {
        public DepartmentType DepartmentType { get; set; }
        public List<Employe> Employes { get; set; }
        public string DepartmentName { get; set; }
        public int MaximumCountEmployes { get; set; }
        public int HourStartWorking { get; set; }
        public int HourEndWorking { get; set; }
    }
}