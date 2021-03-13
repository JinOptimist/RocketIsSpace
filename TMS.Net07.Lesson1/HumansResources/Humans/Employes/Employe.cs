using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    public class Employe : IEmploye
    {
        public Person Person { get; set; }
        public Specification Specification { get; set; }
        public decimal SalaryPerHour { get; set; }

        public Employe()
        {
        }

        public Employe(Person person, Specification specification, decimal salaryPerHour)
        {
            Person = person;
            Specification = specification;
            SalaryPerHour = salaryPerHour;
        }
    }
}