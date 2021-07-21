using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.Human;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.EfStuff
{
    public static class SeedTestExtension
    {
        private static Random _random = new Random();
        private static DateTime defaultInviteDate = new DateTime(2020, 1, 1);
        public static IHost SeedTestData(this IHost server)
        {
            using (var serviceScope = server.Services.CreateScope())
            {
                AddTestDepartments(serviceScope.ServiceProvider);
                AddTestCLients(serviceScope.ServiceProvider);
                AddTestRocket(serviceScope.ServiceProvider);
            }
            return server;
        }

        private static void AddTestDepartments(IServiceProvider service)
        {
            var departmentRepository = service.GetService<IDepartmentRepository>();
            new List<Department> {
            new Department()
            {
                DepartmentName = "Rocket Manufactory",
                DepartmentSpecificationType = DepartmentType.Manufactory
            },
             new Department()
            {
                DepartmentName = "Rocket Bank",
                DepartmentSpecificationType = DepartmentType.Bank
            }
            }.ForEach(department =>
            {
                if (departmentRepository.Get(department.DepartmentName) == null)
                {
                    department.MaximumCountEmployes = _random.Next(10, 100);
                    department.Employes = new List<Employe>();
                    AddTestEmployes(service, department);
                    department.HourStartWorking = 8;
                    department.HourEndWorking = 17;
                    departmentRepository.Save(department);
                }
            });
        }

        private static Department AddTestEmployes(IServiceProvider service, Department department)
        {
            var userReposirory = service.GetService<IUserRepository>();

            //add leader to each department
            if (department.Employes.Where(e => e.Position == Position.Leader).Count() == 0)
            {
                var user = GetRandomUser(service);
                user.Employe = new Employe
                {
                    Position = Position.Leader,
                    SalaryPerHour = _random.Next(100, 500),
                    EmployeStatus = EmployeStatus.Accepted,
                    Department = department,
                    InviteDate = defaultInviteDate
                };
                userReposirory.Save(user);
            }

            //add 4 employes to each department
            while (department.Employes.Count < 5)
            {
                var user = GetRandomUser(service);
                user.Employe = new Employe
                {
                    Position = (Position)_random.Next(2, 5),
                    SalaryPerHour = _random.Next(50, 250),
                    EmployeStatus = EmployeStatus.Accepted,
                    Department = department,
                    InviteDate = defaultInviteDate
                };
                userReposirory.Save(user);
            }
            return department;
        }

        private static void AddTestCLients(IServiceProvider service)
        {
            var userReposirory = service.GetService<IUserRepository>();

            //create 5 clients
            while (userReposirory.GetAll().Where(x => x.Client != null).Count() < 5)
            {
                var user = GetRandomUser(service);
                user.Client = new Client
                {
                    Orders = new List<Order>
                {
                    new Order()
                    {
                        Name = "Buy Rocket",
                        Price = _random.Next(1000000, 1000000000),
                        OrderDateTime = DateTime.Today.AddDays(_random.Next(1, 365))
                    }
                }
                };
                userReposirory.Save(user);
            }
        }

        private static User GetRandomUser(IServiceProvider service)
        {
            List<string> UserNames = new List<string> { "Ivan", "Peter", "Anton", "Roman", "Pavel", "Victor", "Oleg", "Nikita" };
            List<string> UserSurname = new List<string> { "Ivanov", "Petrov", "Sidorov", "Volkov", "Sokolov", "Popov", "Smirnov" };
            var userReposirory = service.GetService<IUserRepository>();
            int attempts = 0;
            int countAttempts = 1000;
            while (attempts < countAttempts)
            {
                string userName = GetRandomFromArray(UserNames);
                string userSurname = GetRandomFromArray(UserSurname);
                string userLogin = string.Concat(userName, userSurname);
                var user = userReposirory.Get(userLogin);

                if (user != null && (countAttempts - attempts) > 1)
                {
                    attempts++;
                    continue;
                };

                return new User()
                {
                    Login = userLogin,
                    Name = userName,
                    SurName = userSurname,
                    Password = "123",
                    Email = $"{userLogin}@rocket.com",
                    Age = _random.Next(18, 60)
                };
            }
            return null;
        }

        private static T GetRandomFromArray<T>(List<T> list)
        {
            var index = _random.Next(0, list.Count());
            return list[index];
        }

        private static Rocket GetRandomRocket(IServiceProvider service) 
        {
            List<string> RocketNames = new List<string> { "Dragon", "Smile", "FlyDubai", "SpaceY", "Eagle", "Alfa", "Charlie" };
            List<string> RocketUrls = new List<string> { "https://i.insider.com/608d79c734af8d001859a6db?width=700", "https://e3.365dm.com/18/07/2048x1152/skynews-nasa-challenger_4373911.jpg" };

            string rocketName = GetRandomFromArray(RocketNames);
            decimal rocketCost = _random.Next(100000, 10000000);
            string rocketUrl = GetRandomFromArray(RocketUrls);
            bool rocketIsReady = _random.Next(2) < 2 ? true : false;
            int rocketCount = _random.Next(1, 50);
            User rocketAuthor = GetRandomUser(service);
            User rocketQa = GetRandomUser(service);

            var rocket = new Rocket()
            {
                Name = rocketName,
                Cost = rocketCost,
                Url = rocketUrl,
                IsReady = rocketIsReady,
                Count = rocketCount,
                Author = rocketAuthor,
                Qa = rocketQa,
            };

            return rocket;
        }

        private static void AddTestRocket(IServiceProvider service) 
        {
            var shopRocketRepository = service.GetService<IShopRocketRepository>();

            int iteration = 0;
            int maxIterations = 100;

            while (shopRocketRepository.GetAll().Where(rocket => rocket != null).Count() < 5 && iteration < maxIterations) 
            {
                var rocket = GetRandomRocket(service);
                shopRocketRepository.Save(rocket);
                iteration++;
            }
        }
    }
}