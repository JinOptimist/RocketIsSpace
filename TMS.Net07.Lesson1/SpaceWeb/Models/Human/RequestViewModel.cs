
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SpaceWeb.Models.Human
{
    public class RequestViewModel
    {
        public long Id { get; set; }
        public long ForeignKeyUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AvatarUrl { get; set; }
        public Position Position { get; set; }
        public decimal SalaryPerHour { get; set; }
        public EmployeStatus EmployeStatus { get; set; }
        public SelectList Department { get; set; }
        public SelectListItem test { get; set; }
    }
}