using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    public class Employe : Person
    {
        public enum Specification
        {
            Leader,
            Spaceman,
            Scientist,
            Еngineer,
            Technicist,
            Other,
            Unknown
        };

        public Person Person { get; }
        public Specification SpecificationType { get; set; }
        public double SalaryPerHour { get; set; }

        public Employe()
        {
        }

        public Employe(Person person)
        {
            Person = person;
            SpecificationType = Specification.Unknown;
        }

        public Employe(Person person, Specification specificationType, double salaryPerHour)
        {
            Person = person;
            SpecificationType = specificationType;
            SalaryPerHour = salaryPerHour;
        }
    }
}