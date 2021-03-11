using System;
using System.Linq;
using System.Collections.Generic;

namespace HumansResources.Humans.Employes
{
    public class Department
    {
        public enum DepartmentType
        {
            Manufactory,
            Laboratory,
            MissionControlCenter,
            SpacecraftCrew,
            Other
        };

        public DepartmentType Dtype { get; set; }
        public string DepartmentName { get; set; }
        public int MaximumCountEmployes { get; set; } = 1;
        public int HourStartWorking { get; set; } = 9;
        public int HourEndWorking { get; set; } = 17;
        private readonly List<Employe> _listEmployes = new List<Employe>();


        public Department()
        {
        }

        public Department(DepartmentType departmentType)
        {
            Dtype = departmentType;
        }

        public Department(DepartmentType departmentType, string departmentName, int maximumCountEmploes)
        {
            Dtype = departmentType;
            DepartmentName = departmentName;
            if (maximumCountEmploes > 0)
            {
                MaximumCountEmployes = maximumCountEmploes;
            }
        }

        public void SetEmploye(Employe employe, out bool result)
        {
            if (_listEmployes.Count() >= MaximumCountEmployes ||
                employe.SpecificationType == Specification.Unknown ||
                (Dtype == DepartmentType.SpacecraftCrew &&
                employe.SpecificationType != Specification.Spaceman))
            {
                result = false;
                return;
            }
            _listEmployes.Add(employe);
            result = true;
        }

        public int GetCountEmployes(Specification specificationType)
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