namespace SpaceWeb.EfStuff.Model
{
    public class Employe : BaseModel
    {
        public Specification Specification { get; set; }
        public Department Department { get; set; }
        public User User { get; set; } 
        public decimal SalaryPerHour { get; set; }
    }
}