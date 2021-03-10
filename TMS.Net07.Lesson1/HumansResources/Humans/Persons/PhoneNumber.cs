
using System.Text.RegularExpressions;

namespace HumansResources.Humans.Persons
{
    public class PhoneNumber : IValidator
    {
        public string Number { get; set; }

        public string Pattern { get => @"^(?<countryCode>\d{1,3})(?<operatorCode>\d{2})(?<phoneNumber>\d{7})$"; }

        public PhoneNumber()
        {
            Number = "";
        }

        public PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public bool Validation()
        {
            return Regex.IsMatch(Number, Pattern);
        }

        public override string ToString()
        {
            return Number;
        }
    }
}