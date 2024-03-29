﻿using Microsoft.Extensions.DependencyInjection;
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
    public static class SeedExtension
    {
        public const string AdminName = "admin";
        public static IHost SeedData(this IHost server)
        {
            using (var serviceScope = server.Services.CreateScope())
            {
                SetDefaultUser(serviceScope.ServiceProvider);
                SetDefaultInsuranceType(serviceScope.ServiceProvider);
                SetDefaultExchangeRateToUsdCurrent(serviceScope.ServiceProvider);
                AddTestExchangeRatesHistory(serviceScope.ServiceProvider);
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
            var currencyService = services.GetService<ICurrencyService>();

            if (!currencyService.CheckInternetConnection())
            {
                return;
            }
            var exchRateToUsdCurrentRepository = services.GetService<ExchangeRateToUsdCurrentRepository>();

            currencyService.DeleteCurrentExchRatesFromDb(exchRateToUsdCurrentRepository);

            var gottenCurrencies = currencyService.GetExchangeRates();
            currencyService.PutCurrentExchangeRatesToDb(exchRateToUsdCurrentRepository, gottenCurrencies);
        }

        private static void AddTestExchangeRatesHistory(IServiceProvider service)
        {
            var exchangeRateToUsdHistoryRepository = service.GetService<IExchangeRateToUsdHistoryRepository>();

            SaveExchangeRateHistory(Currency.BYN, TypeOfExchange.Buy, 2.51m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.BYN, TypeOfExchange.Sell, 2.47m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.USD, TypeOfExchange.Buy, 1m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.USD, TypeOfExchange.Sell, 1m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.EUR, TypeOfExchange.Buy, 0.86m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.EUR, TypeOfExchange.Sell, 0.84m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.PLN, TypeOfExchange.Buy, 3.98m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.PLN, TypeOfExchange.Sell, 3.81m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.GBP, TypeOfExchange.Buy, 0.74m, "25-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.GBP, TypeOfExchange.Sell, 0.71m, "25-07-2021", exchangeRateToUsdHistoryRepository);

            SaveExchangeRateHistory(Currency.BYN, TypeOfExchange.Buy, 2.53m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.BYN, TypeOfExchange.Sell, 2.44m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.USD, TypeOfExchange.Buy, 1m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.USD, TypeOfExchange.Sell, 1m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.EUR, TypeOfExchange.Buy, 0.86m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.EUR, TypeOfExchange.Sell, 0.82m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.PLN, TypeOfExchange.Buy, 3.95m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.PLN, TypeOfExchange.Sell, 3.82m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.GBP, TypeOfExchange.Buy, 0.72m, "26-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.GBP, TypeOfExchange.Sell, 0.73m, "26-07-2021", exchangeRateToUsdHistoryRepository);

            SaveExchangeRateHistory(Currency.BYN, TypeOfExchange.Buy, 2.50m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.BYN, TypeOfExchange.Sell, 2.42m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.USD, TypeOfExchange.Buy, 1m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.USD, TypeOfExchange.Sell, 1m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.EUR, TypeOfExchange.Buy, 0.88m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.EUR, TypeOfExchange.Sell, 0.85m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.PLN, TypeOfExchange.Buy, 3.96m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.PLN, TypeOfExchange.Sell, 3.81m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.GBP, TypeOfExchange.Buy, 0.72m, "27-07-2021", exchangeRateToUsdHistoryRepository);
            SaveExchangeRateHistory(Currency.GBP, TypeOfExchange.Sell, 0.70m, "27-07-2021", exchangeRateToUsdHistoryRepository);
        }

        private static void SaveExchangeRateHistory(
            Currency currency, TypeOfExchange typeOfExchange, decimal exchRate, string exchRateDate, 
            IExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository)
        {
            var exchangeRate = new ExchangeRateToUsdHistory()
            {
                Currency = currency,
                TypeOfExch = typeOfExchange,
                ExchRate = exchRate,
                ExchRateDate = Convert.ToDateTime(exchRateDate)
            };
            exchangeRateToUsdHistoryRepository.Save(exchangeRate);
        }

        //private static void SetDefaultEmploye(IServiceProvider service)
        //{
        //    var userReposirory = service.GetService<IUserRepository>();
        //    var departmentRepository = service.GetService<IDepartmentRepository>();
        //    var user = userReposirory.Get(string.Concat(EmployeName, EmployeSurname));
        //    if (user == null)
        //    {
        //        user = CreateUser(string.Concat(EmployeName, EmployeSurname), EmployeName, EmployeSurname, DefaultPassword);
        //        user.Employe = CreateEmploye(Position.Leader, EmployeStatus.Accepted, 200, departmentRepository.Get(DepartmentName));
        //    }
        //    else if (user.Employe == null)
        //    {
        //        user.Employe = CreateEmploye(Position.Leader, EmployeStatus.Accepted, 200, departmentRepository.Get(DepartmentName));
        //    }
        //    userReposirory.Save(user);
        //}

        //private static void SetDefaultCient(IServiceProvider service)
        //{
        //    var userReposirory = service.GetService<IUserRepository>();
        //    var user = userReposirory.Get(string.Concat(ClientName, ClientSurname));
        //    if (user == null)
        //    {
        //        user = CreateUser(string.Concat(ClientName, ClientSurname), ClientName, ClientSurname, DefaultPassword);
        //        user.Client = CreateClient();
        //    }
        //    else if (user.Client == null)
        //    {
        //        user.Client = CreateClient();
        //    }
        //    userReposirory.Save(user);
        //}

        private static User CreateUser(string Login, string Name, string SurName, string Password, string Email = "", int Age = 0) =>
            new User
            {
                Login = Login,
                Name = Name,
                SurName = SurName,
                Password = Password,
                Email = Email,
                Age = Age
            };

        private static Client CreateClient() =>
            new Client
            {
                Orders = new List<Order>
                {
                    new Order(){ Name="Big rocket", Price=4333, OrderDateTime=DateTime.Today },
                    new Order(){ Name="Medium rocket", Price=2341, OrderDateTime=DateTime.Today },
                    new Order(){ Name="Small rocket", Price=932, OrderDateTime=DateTime.Today },
                }
            };

        private static Employe CreateEmploye(Position position, EmployeStatus employeStatus, decimal salaryPerHour, Department department) =>
            new Employe
            {
                Position = position,
                SalaryPerHour = salaryPerHour,
                EmployeStatus = employeStatus,
                Department = department
            };
    }
}
