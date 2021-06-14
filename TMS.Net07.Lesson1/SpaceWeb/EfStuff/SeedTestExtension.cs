using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.Human;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.Service;

namespace SpaceWeb.EfStuff
{
    public static class SeedTestExtension
    {
        private static Random _random = new Random();
        private static List<string> Names = new List<string> { "Ivan", "Petr", "POl" };

        public static IHost SeedTestData(this IHost server)
        {
            using (var serviceScope = server.Services.CreateScope())
            {
                //AddDefaultEmploye(serviceScope.ServiceProvider);
            }

            return server;
        }

        private static void AddDefaultEmploye(IServiceProvider service)
        {
            var userReposirory = service.GetService<IUserRepository>();
            var departmentRepository = service.GetService<IDepartmentRepository>();
            var departments = departmentRepository.GetAll().ToList();
            var e = new Employe()
            {
                Department = GetRandomFromArray(departments)
            };
            var user = new User()
            {
                Age = _random.Next(20, 100),
                Name = GetRandomFromArray(Names),
                Employe = e
            };

            userReposirory.Save(user);
        }

        private static T GetRandomFromArray<T>(List<T> list)
        {
            var index = _random.Next(0, list.Count());
            return list[index];
        }
    }
}
