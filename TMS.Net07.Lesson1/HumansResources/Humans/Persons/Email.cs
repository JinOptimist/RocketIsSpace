using System;

namespace HumansResources.Humans.Persons
{
    public class Email : IValidator
    {
        public string EmailAddress { get; set; }

        string IValidator.pattern => throw new NotImplementedException();
    }
}