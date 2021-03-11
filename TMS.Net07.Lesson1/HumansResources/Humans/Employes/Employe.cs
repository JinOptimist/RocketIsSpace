using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    public class Employe : Person
    {
        public Specification SpecificationType { get; set; }
        public double SalaryPerHour { get; set; }

        public Employe(PhoneNumber phoneNumber, PostAddress postAddress, Email email) : base(phoneNumber, postAddress, email) 
        {
            SpecificationType = Specification.Unknown;
        }

        public Employe(PhoneNumber phoneNumber, PostAddress postAddress, Email email, Specification specificationType, double salaryPerHour) : this(phoneNumber, postAddress, email)
        {
            SpecificationType = specificationType;
            SalaryPerHour = salaryPerHour;
        }
    }
}