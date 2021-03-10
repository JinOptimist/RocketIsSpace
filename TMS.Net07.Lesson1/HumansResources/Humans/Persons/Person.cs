
using System;

namespace HumansResources.Humans.Persons
{
    public class Person : IContact
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        /// <summary>
        /// 0 - Female, 1 - Male
        /// </summary>
        public bool? Sex { get; set; } // nullable типы https://metanit.com/sharp/tutorial/2.17.php

        public DateTime BirthDate { get; set; }

        public PhoneNumber PhoneNumber { get; }

        public PostAddress PostAddress { get; }

        public Email Email { get; }

        public Person()
        {
            Name = null;
            Surname = null;
            Sex = null;
        }

        public Person(PhoneNumber phoneNumber, PostAddress postAddress, Email email) : this()
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