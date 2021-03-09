using HumansResources.Humans.Persons;
using System;

namespace HumansResources.Humans.EmployesVariant
{
    public class Employe : Person
    {
        public enum Specification
        {
            Leader,
            Spaceman,
            Scientist,
            Еngineer,
            Technicist,
            Other,
            Unknown
        };  
        
        public Person Person { get; set; }        
        public Specification SpecificationEmploye { get; set; }
        public double SalaryPerHour { get; set; }

        public Employe()
        {
        }
        
        public Employe(Person person)
        {
            Person = person;
            SpecificationEmploye = Specification.Unknown;
        }

        public Employe(Person person, Specification specificationEmploye)
        {
            Person = person;
            SpecificationEmploye = specificationEmploye;            
        }

        public decimal GetCountWorkingHours(DateTime dateStart, DateTime dateEnd)
        {
            int countHours = 0;
            while (DateTime.Compare(dateStart, dateEnd) < 0)
            {
                int hourStart = 9;
                int hourEnd = 17;
                dateStart = dateStart.AddHours(1);
                if (dateStart.DayOfWeek != DayOfWeek.Saturday &&
                    dateStart.DayOfWeek != DayOfWeek.Sunday &&
                    dateStart.Hour > (hourStart - 1) && dateStart.Hour < hourEnd)
                {
                    ++countHours;
                }
            }
            return countHours;
        }
    }   
}