using System;
using NUnit.Framework;
using HumansResources.Humans.Employes;
using System.Collections.Generic;
using Moq;

namespace HumansResources.Test.Humans.Employes
{
    class DepartmentTest
    {
        [Test]
        [TestCase("2021-02-01", "2021-02-28", 22400)]
        public void GetCostWorkingDepartment(string dateStartStr, string DateEndStr, decimal expecredResult)
        {
            List<IEmploye> list = new List<IEmploye>();
            var employe1 = new Mock<IEmploye>();
            employe1.Setup(x => x.SalaryPerHour).Returns(140);
            employe1.Setup(x => x.SpecificationType).Returns(Specification.Scientist);
            list.Add(employe1.Object);
            Department department = new Department(Department.DepartmentType.Laboratory, "Laboratory#1", 2);
            department.SetEmploye(list[0], out bool result);
            DateTime.TryParse(dateStartStr, out DateTime dateStart);
            DateTime.TryParse(DateEndStr, out DateTime dateEnd);
            Assert.AreEqual(expecredResult, department.GetCostWorkingDepartment(dateStart, dateEnd));
        }
    }
}