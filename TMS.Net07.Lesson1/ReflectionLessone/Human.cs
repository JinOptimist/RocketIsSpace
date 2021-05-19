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

        public static string MagicName { get; set; }
    }
}
