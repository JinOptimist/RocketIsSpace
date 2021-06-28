using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionLessone
{
    class Human
    {
        private readonly int _yearOfBirthday;

        public Human(int yearOfBirthday)
        {
            _yearOfBirthday = yearOfBirthday;
        }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - _yearOfBirthday;
            }
        }

        public string FullName { get; set; }

        public string SecondName { get; set; }

        public static string MagicName { get; set; }



        public override int GetHashCode()
        {
            return FullName.GetHashCode() & Age.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var human1 = this;
            var human2 = obj as Human;
            return human1.FullName == human2.FullName 
                && human1.Age == human2.Age;
        }
    }
}
