﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff
{
    public static class SeedExtension
    {
        public const string AdminName = "admin";
        public static IHost SeedData(this IHost server)
        {
            using (var serviceScope = server.Services.CreateScope())
            {
                SetDefaultUser(serviceScope.ServiceProvider);
            }

            return server;
        }

        private static void SetDefaultUser(IServiceProvider services)
        {
            var userRepository = services.GetService<IUserRepository>();

            var admin = userRepository.Get(AdminName);
            if (admin == null)
            {
                admin = new User()
                {
                    Login = AdminName,
                    Name = AdminName,
                    Password = "123",
                    Age = 100,
                    JobType = JobType.Admin
                };
                userRepository.Save(admin);
            }
        }
    }
}
