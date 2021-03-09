using System;
using HumansResources.Humans.EmployesVariant;

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
            Employe e = new Employe(null, Employe.Specification.Unknown);
            DateTime.TryParse("2021-02-01 10:00", out DateTime dateStart);
            DateTime.TryParse("2021-02-28", out DateTime dateEnd);
            var result = e.GetCountWorkingHours(dateStart, dateEnd);
            Console.WriteLine("result = " + result);
            Department d = new Department(Department.Type.Laboratory, 10);
            d.SetEmploye(new Employe(null));
            d.SetEmploye(new Employe(null, Employe.Specification.Technicist));
            d.SetEmploye(new Employe(null, Employe.Specification.Technicist));
            int count = d.GetCountEmployes(Employe.Specification.Technicist);
            Console.WriteLine("count = " + count);
        }
    }
}