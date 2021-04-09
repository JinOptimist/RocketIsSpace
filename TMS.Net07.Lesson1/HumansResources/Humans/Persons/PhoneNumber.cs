
namespace HumansResources.Humans.Persons
{
    public class PhoneNumber
    {
        public string Number { get; }

        public PhoneNumber() { }

        public PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}