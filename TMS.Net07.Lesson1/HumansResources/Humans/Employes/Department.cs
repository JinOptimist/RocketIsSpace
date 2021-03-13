using System;
using System.Linq;
using System.Collections.Generic;

namespace HumansResources.Humans.Employes
{
    public class Department
    {

        public DepartmentType DepartmentType { get; set; }
        public string DepartmentName { get; set; }
        public int MaximumCountEmployes { get; set; } = 1;
        public int HourStartWorking { get; set; } = 9;
        public int HourEndWorking { get; set; } = 17;
        private readonly List<IEmploye> _listEmployes = new List<IEmploye>();

        public Department()
        {
        }

        public Department(DepartmentType departmentType, string departmentName, int maximumCountEmploes)
        {
            DepartmentType = departmentType;
            DepartmentName = departmentName;
            if (maximumCountEmploes > 0)
            {
                MaximumCountEmployes = maximumCountEmploes;
            }
        }

        public void SetEmploye(IEmploye employe, out bool result)
        {
            if (_listEmployes.Count() >= MaximumCountEmployes ||
                employe.Specification == Specification.Unknown ||
                (DepartmentType == DepartmentType.SpacecraftCrew &&
                employe.Specification != Specification.Spaceman))
            {
                result = false;
                return;
            }
            _listEmployes.Add(employe);
            result = true;
        }

        public int GetCountEmployes(Specification specificationType) =>
            _listEmployes
            .Where(employe => employe.Specification == specificationType)
            .Count();

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

        public decimal GetCostWorkingDepartment(DateTime dateStart, DateTime dateEnd) =>
            _listEmployes
            .Sum(employe => employe.SalaryPerHour)
            * GetCountWorkingHours(dateStart, dateEnd);
    }
}