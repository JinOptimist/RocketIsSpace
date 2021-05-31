using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentType = SpaceWeb.EfStuff.Model.DepartmentType;

namespace SpaceWeb.EfStuff
{
    public static class SeedExtension
    {
        public const string AdminName = "admin";
        public const string DepartmentName = "Administration";
        public const string EmployeName = "Test";
        public const string EmployeSurname = "Employe";
        public static IHost SeedData(this IHost server)
        {
            using (var serviceScope = server.Services.CreateScope())
            {
                SetDefaultUser(serviceScope.ServiceProvider);
                SetDefaultDepartment(serviceScope.ServiceProvider);
                SetDefaultInsuranceType(serviceScope.ServiceProvider);
                SetDefaultExchangeRateToUsdCurrent(serviceScope.ServiceProvider);
                SetDefaultEmploye(serviceScope.ServiceProvider);
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

            var chiefBankEmployee = userRepository.Get("chiefBankEmployee");
            if (chiefBankEmployee == null)
            {
                chiefBankEmployee = new User()
                {
                    Login = "ChiefBankEmployee",
                    Name = "ChiefBankEmployee",
                    Password = "123",
                    Age = 100,
                    JobType = JobType.ChiefBankEmployee
                };
                userRepository.Save(chiefBankEmployee);
            }
        }

        private static void SetDefaultDepartment(IServiceProvider services)
        {
            var departmentRepository = services.GetService<IDepartmentRepository>();
            string defaultDepartmentName = DepartmentName;
            var department = departmentRepository.Get(defaultDepartmentName);
            if (department == null)
            {
                department = new Department
                {
                    DepartmentName = defaultDepartmentName,
                    DepartmentType = DepartmentType.Other,
                    MaximumCountEmployes = 1,
                    HourStartWorking = 8,
                    HourEndWorking = 17
                };
                departmentRepository.Save(department);
            }
        }

        private static void SetDefaultInsuranceType(IServiceProvider services)
        {
            var insuranceTypeRepository = services.GetService<InsuranceTypeRepository>();

            var insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Start, InsurancePeriod.Six);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Start,
                    Cost = 15,
                    InsurancePeriod = InsurancePeriod.Six,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Start, InsurancePeriod.Twelve);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Start,
                    Cost = 30,
                    InsurancePeriod = InsurancePeriod.Twelve,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Casco, InsurancePeriod.Six);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Casco,
                    Cost = 100,
                    InsurancePeriod = InsurancePeriod.Six,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Casco, InsurancePeriod.Twelve);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Casco,
                    Cost = 200,
                    InsurancePeriod = InsurancePeriod.Twelve,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Medhelp, InsurancePeriod.Six);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Medhelp,
                    Cost = 250,
                    InsurancePeriod = InsurancePeriod.Six,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Medhelp, InsurancePeriod.Twelve);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Medhelp,
                    Cost = 500,
                    InsurancePeriod = InsurancePeriod.Twelve,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Air, InsurancePeriod.Six);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Air,
                    Cost = 5000,
                    InsurancePeriod = InsurancePeriod.Six,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }

            insurancePolis = insuranceTypeRepository.GetPolis(InsuranceNameType.Air, InsurancePeriod.Twelve);
            if (insurancePolis == null)
            {
                insurancePolis = new InsuranceType()
                {
                    InsuranceNameType = InsuranceNameType.Air,
                    Cost = 10000,
                    InsurancePeriod = InsurancePeriod.Twelve,
                };
                insuranceTypeRepository.Save(insurancePolis);
            }
        }

        private static void SetDefaultExchangeRateToUsdCurrent(IServiceProvider services)
        {

            var exchangeRateToUsdCurrentRepository = services.GetService<ExchangeRateToUsdCurrentRepository>();

            var exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.BYN, TypeOfExchange.Sell);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.BYN,
                    TypeOfExch = TypeOfExchange.Sell,
                    ExchRate = 2.505m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.BYN, TypeOfExchange.Buy);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.BYN,
                    TypeOfExch = TypeOfExchange.Buy,
                    ExchRate = 2.510m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.USD, TypeOfExchange.Sell);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.USD,
                    TypeOfExch = TypeOfExchange.Sell,
                    ExchRate = 1m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.USD, TypeOfExchange.Buy);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.USD,
                    TypeOfExch = TypeOfExchange.Buy,
                    ExchRate = 1m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.EUR, TypeOfExchange.Sell);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.EUR,
                    TypeOfExch = TypeOfExchange.Sell,
                    ExchRate = 0.818m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.EUR, TypeOfExchange.Buy);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.EUR,
                    TypeOfExch = TypeOfExchange.Buy,
                    ExchRate = 0.824m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.PLN, TypeOfExchange.Sell);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.PLN,
                    TypeOfExch = TypeOfExchange.Sell,
                    ExchRate = 2851.17m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.PLN, TypeOfExchange.Buy);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.PLN,
                    TypeOfExch = TypeOfExchange.Buy,
                    ExchRate = 2853.75m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.GBP, TypeOfExchange.Sell);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.GBP,
                    TypeOfExch = TypeOfExchange.Sell,
                    ExchRate = 361.21m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }

            exchangeRate = exchangeRateToUsdCurrentRepository.GetExchangeRate(Currency.GBP, TypeOfExchange.Buy);
            if (exchangeRate == null)
            {
                exchangeRate = new ExchangeRateToUsdCurrent()
                {
                    Currency = Currency.GBP,
                    TypeOfExch = TypeOfExchange.Buy,
                    ExchRate = 361.83m,
                };
                exchangeRateToUsdCurrentRepository.Save(exchangeRate);
            }
        }

        private static void SetDefaultEmploye(IServiceProvider service)
        {
            var userReposirory = service.GetService<IUserRepository>();
            var departmentRepository = service.GetService<IDepartmentRepository>();
            var user = userReposirory.Get(string.Concat(EmployeName, EmployeSurname));
            if (user == null)
            {
                user = new User()
                {
                    Login = string.Concat(EmployeName,EmployeSurname),
                    Name = EmployeName,
                    SurName = EmployeSurname,
                    Password = "1111",
                    Employe = CreateEmploye(departmentRepository)
                };
            }
            else if (user != null && user.Employe == null)
            {
                user.Employe = CreateEmploye(departmentRepository);
            }
            userReposirory.Save(user);
        }

        private static Employe CreateEmploye(IDepartmentRepository departmentRepository) =>
            new Employe
            {
                Specification = Specification.Leader,
                SalaryPerHour = 200,
                Department = departmentRepository.Get(DepartmentName)
            };
    }
}
