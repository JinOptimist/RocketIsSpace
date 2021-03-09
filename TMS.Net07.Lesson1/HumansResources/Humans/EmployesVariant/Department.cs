using System;
using System.Linq;
using System.Collections.Generic;

namespace HumansResources.Humans.EmployesVariant
{
    public class Department
    {
        public enum Type { 
            Manufactory,
            Laboratory,
            MissionControlCenter,
            SpacecraftCrew 
        };
        
        public int MaximumCountEmployes { get; set; } = 1;            
        public Type DepartmentType { get; }
        public string DepartmentName { get; set; }
        public int HourStartWorking { get; set; } = 9;
        public int HourEndWorking { get; set; } = 17;
        private readonly List<Employe> _listEmployes = new List<Employe>();


        public Department() { 
        }

        public Department(Type departmentType)
        {
            DepartmentType = departmentType;
        }

        public Department(Type departmentType, int maximumCountEmploes)
        {
            DepartmentType = departmentType;
            if (maximumCountEmploes > 0)
            {
                MaximumCountEmployes = maximumCountEmploes;
            }            
        }        
        public bool SetEmploye(Employe employe) 
        {
            if (_listEmployes.Count() >= MaximumCountEmployes ||
                employe.SpecificationType == Employe.Specification.Unknown ||
                (DepartmentType == Type.SpacecraftCrew &&
                employe.SpecificationType != Employe.Specification.Spaceman))
            {
                return false;
            }
            _listEmployes.Add(employe);
            return true;
        }
       
        public int GetCountEmployes(Employe.Specification specificationType) 
        {                        
            return _listEmployes.Where(employe => employe.SpecificationType == specificationType).Count();
        }

        public int GetCountWorkingHours(DateTime dateStart, DateTime dateEnd)
        {
            int countHours = 0;
            while (DateTime.Compare(dateStart, dateEnd) < 0)
            {
                dateStart = dateStart.AddHours(1);
                if (dateStart.DayOfWeek != DayOfWeek.Saturday &&
                    dateStart.DayOfWeek != DayOfWeek.Sunday &&
                    dateStart.Hour > HourStartWorking && dateStart.Hour <= HourEndWorking)
                {
                    countHours++;                    
                } 
            }
            return countHours;
        }

        public decimal GetCostWorkingDepartment(DateTime dateStart, DateTime dateEnd) 
        {
            decimal salary = (decimal)_listEmployes.Sum(employe => employe.SalaryPerHour);
            return salary * GetCountWorkingHours(dateStart, dateEnd);
        }
    }
}