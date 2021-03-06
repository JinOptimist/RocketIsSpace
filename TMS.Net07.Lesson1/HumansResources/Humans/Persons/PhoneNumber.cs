
namespace HumansResources.Humans.Persons
{
    public class PhoneNumber : IValidator
    {
        string phoneNumber;
        string IValidator.pattern { get => @"(?<countryCode>\d{1,3})(?<operatorCode>\d{2})(?<phoneNumber>\d{7})$"; }
        public PhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            ((IValidator)this).Validation(phoneNumber);
        }
        public override string ToString()
        {
            return phoneNumber;
        }
    }
}