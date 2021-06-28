
namespace SpaceWeb.Models.Human
{
    public class ShortEmployeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public decimal? SalaryPerHour { get; set; }
        public string AvatarUrl { get; set; }
    }
}