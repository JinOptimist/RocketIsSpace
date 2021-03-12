using HumansResources.Humans.Persons;

namespace HumansResources.Humans.Employes
{
    public class Employe : Person, IEmploye
    {
        public Specification SpecificationType { get; set; }
        public decimal SalaryPerHour { get; set; }

        public Employe(PhoneNumber phoneNumber, PostAddress postAddress, Email email) : base(phoneNumber, postAddress, email)
        {
            SpecificationType = Specification.Unknown;
        }

        public Employe(PhoneNumber phoneNumber, PostAddress postAddress, Email email, Specification specificationType, decimal salaryPerHour) : this(phoneNumber, postAddress, email)
        {
            SpecificationType = specificationType;
            SalaryPerHour = salaryPerHour;
        }
    }
}