
namespace HumansResources.Humans.Persons
{
    public class Email
    {
        public string EmailAddress { get; set; }

        public Email() { }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}