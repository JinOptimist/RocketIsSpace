using System;

namespace HumansResources.Humans.Persons
{
    public class Person : IContact
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public PhoneNumber PhoneNumber { get; }

        public PostAddress PostAddress { get; }

        public Email Email { get; }

        public Person(PhoneNumber phoneNumber, PostAddress postAddress, Email email)
        {
            PhoneNumber = phoneNumber;
            PostAddress = postAddress;
            Email = email;
        }

        public override string ToString()
        {
            return $"Фамилия: {Surname}, Имя {Name}" +
                $"Год рождения: {BirthDate.Year}, месяц {BirthDate.Month}, число {BirthDate.Day}, пол {Sex}" +
                $"Почта: {Email}" +
                $"Телефон: {PhoneNumber}" +
                $"Сведения о почтовом адресе\n{PostAddress}";
        }
    }
}