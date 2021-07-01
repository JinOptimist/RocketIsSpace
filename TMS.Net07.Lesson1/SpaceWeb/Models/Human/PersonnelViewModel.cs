using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Human
{
    public class PersonnelViewModel
    {
        public DepartmentViewModel Department { get; set; }
        public List<RequestViewModel> RequestsToEmploy { get; set; }
    }
}
