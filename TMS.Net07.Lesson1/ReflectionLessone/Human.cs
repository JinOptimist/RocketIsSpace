using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionLessone
{
    class Human
    {
        private int _yearOfBirthday;

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

        public static Human operator +(Human human1, Human human2)
        {
            var child = new Human(DateTime.Now.Year);
            child.SecondName = human1.SecondName;
            child.FullName = human2.FullName;
            return child;
        }

        public static Human operator +(Human human1, int year)
        {
            human1._yearOfBirthday += year;
            return human1;
        }

        public static Human operator +(int year, Human human1)
        {
            human1._yearOfBirthday += year;
            return human1;
        }

        public static bool operator ==(Human human1, Human human2)
        {
            return human1.Equals(human2);
        }

        public static bool operator !=(Human human1, Human human2)
        {
            return !human1.Equals(human2);
        }

    }
}
