using System;
using NUnit.Framework;
using HumansResources.Humans.Employe;

namespace HumansResources.Test.Humans.Employes
{
    class DepartmentTest
    {
        [Test]
        public void GetCostWorkingDepartment()
        {
            double salaryDepartment = 0;
            double[] salariesEmployes = { 33.333, 37.85, 54, 36.3, 27.274 };
            Department department = new Department(Department.DepartmentType.Laboratory, "Laboratory#1", salariesEmployes.Length);
            DateTime.TryParse("2021-02-01", out DateTime dateStart);
            DateTime.TryParse("2021-02-28", out DateTime dateEnd);
            for (int i = 0; i < salariesEmployes.Length; i++)
            {
                department.SetEmploye(new Employe(null, Employe.Specification.Scientist, salariesEmployes[i]), out bool result);
                if (result)
                {
                    salaryDepartment += salariesEmployes[i];
                }
            }
            decimal cost = department.GetCostWorkingDepartment(dateStart, dateEnd);
            Assert.AreEqual((decimal)salaryDepartment * (department.HourEndWorking - department.HourStartWorking) * 20, cost);
        }
    }
}