using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Models.Human
{
    public class ShortEmployeViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Specification Specification { get; set; }
        public decimal SalaryPerHour { get; set; }
    }
}