using HumansResources.Humans.EmployesVariant;
using System;

namespace HumansResources
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.WriteLine("Hello World!");
        }

        static void Test() 
        {
            Department department = new Department(Department.Type.Laboratory, 10);
            DateTime.TryParse("2021-02-01 10:00", out DateTime dateStart);
            DateTime.TryParse("2021-02-28", out DateTime dateEnd);
            var result = department.GetCountWorkingHours(dateStart, dateEnd);
            Console.WriteLine("countWorkingHour = " + result);            
            department.SetEmploye(new Employe(null, Employe.Specification.Technicist, 1));
            department.SetEmploye(new Employe(null, Employe.Specification.Technicist, 10));
            department.SetEmploye(new Employe(null, Employe.Specification.Technicist, 10));
            var countEmployes = department.GetCountEmployes(Employe.Specification.Technicist);
            Console.WriteLine("countEmployes = " + countEmployes);
            decimal cost = department.GetCostWorkingDepartment(dateStart, dateEnd);
            Console.WriteLine("cost = " + cost);
        }
    }
}