using System;
using System.Text.RegularExpressions;

namespace HumansResources.Humans.Persons
{
    //Интерфейс думаю не нужен, т.к. он просто провереряет почту на правильность (не на существование) 
    //впоследствии можно отпавлять по введенному email письмо с проврекой
    public class Email : IValidator
    {
        public string EmailAddress { get; set; }

        public string Pattern { get => @"(?<name>\w+)\@(?<leftDomain>\w+)\.(?<rightDomain>\w+)$"; }

        public Email()
        {
            EmailAddress = "";
        }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
        public bool Validation()
        {
            return Regex.IsMatch(EmailAddress, Pattern);
        }

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}