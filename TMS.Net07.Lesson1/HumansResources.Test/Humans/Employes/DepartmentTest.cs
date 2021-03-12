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
        [TestCase("2021-02-01", "2021-02-28", 20000)]
        [TestCase("2021-02-01", "2021-02-14", 10000)]
        [TestCase("2021-01-01", "2021-01-31", 21000)]
        [TestCase("2021-01-01", "2021-01-15", 10000)]
        public void GetCostWorkingDepartment(string dateStartStr, string DateEndStr, decimal expecredResult)
        {
            List<IEmploye> list = new List<IEmploye>();

            var employe1 = new Mock<IEmploye>();
            employe1.Setup(x => x.SalaryPerHour).Returns(65);
            employe1.Setup(x => x.SpecificationType).Returns(Specification.Scientist);

            var employe2 = new Mock<IEmploye>();
            employe2.Setup(x => x.SalaryPerHour).Returns(60);
            employe2.Setup(x => x.SpecificationType).Returns(Specification.Scientist);

            list.Add(employe1.Object);
            list.Add(employe2.Object);

            Department department = new Department(Department.DepartmentType.Laboratory, "Laboratory#1", 2);
            foreach(var x in list)
                department.SetEmploye(x, out bool result);
            DateTime.TryParse(dateStartStr, out DateTime dateStart);
            DateTime.TryParse(DateEndStr, out DateTime dateEnd);
            Assert.AreEqual(expecredResult, department.GetCostWorkingDepartment(dateStart, dateEnd));
        }
    }
}