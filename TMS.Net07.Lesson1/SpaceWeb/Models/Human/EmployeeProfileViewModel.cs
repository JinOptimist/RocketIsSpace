using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Human
{
    public class EmployeeProfileViewModel
    {
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
        public Specification Specification { get; set; }

        public ClientViewModel Client { get; set; }
    }
}
