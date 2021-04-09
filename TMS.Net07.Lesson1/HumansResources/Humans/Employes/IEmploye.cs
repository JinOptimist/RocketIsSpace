using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    public interface IEmploye
    {
        public Person Person { get; set; }
        decimal SalaryPerHour { get; set; }
        Specification Specification { get; set; }
    }
}