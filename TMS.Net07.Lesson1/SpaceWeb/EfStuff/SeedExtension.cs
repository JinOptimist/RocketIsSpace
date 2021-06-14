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
            var exchRateToUsdCurrentRepository = services.GetService<ExchangeRateToUsdCurrentRepository>();

            currencyService.DeleteCurrentExchRatesFromDb(exchRateToUsdCurrentRepository);

            var gottenCurrencies = currencyService.GetExchangeRates();
            currencyService.PutCurrentExchangeRatesToDb(exchRateToUsdCurrentRepository, gottenCurrencies);
        }
    }
}
