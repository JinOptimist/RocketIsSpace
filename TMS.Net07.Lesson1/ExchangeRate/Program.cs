using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Xml.Serialization;

namespace ExchangeRate
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            var connection = dbContextOptionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SpaceWeb;Trusted_Connection=True");

            SpaceDbContext spaceDbContext = new SpaceDbContext(connection.Options);

            var contextAccessor = new HttpContextAccessor();

            var configExpression = new MapperConfigurationExpression();
            var mapperConfiguration = new MapperConfiguration(configExpression);
            var mapper = new Mapper(mapperConfiguration);

            ExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository =
                new ExchangeRateToUsdCurrentRepository(spaceDbContext);
            ExchangeAccountHistoryRepository exchangeAccountHistoryRepository =
                new ExchangeAccountHistoryRepository(spaceDbContext);
            ExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository =
                new ExchangeRateToUsdHistoryRepository(spaceDbContext);

            TransactionBankRepository transactionBankRepository = new TransactionBankRepository(spaceDbContext);

            BankAccountHistoryRepository bankAccountHistoryRepository = 
                new BankAccountHistoryRepository(spaceDbContext, mapper, contextAccessor);

            var bankAccountRepository = new BankAccountRepository(spaceDbContext, transactionBankRepository, bankAccountHistoryRepository);

            var userRepository = new UserRepository(spaceDbContext, (IBankAccountRepository)bankAccountRepository);

            IUserService userService = new UserService(userRepository, contextAccessor);

            var currencyService =
                new CurrencyService(userService,
                    exchangeRateToUsdCurrentRepository,
                    exchangeAccountHistoryRepository,
                    exchangeRateToUsdHistoryRepository,
                    mapper);

            var currentDate = DateTime.Now;
            while (true)
            {
                if ((currentDate.Minute % 3) == 0)
                {
                    currencyService.MoveCurrentExchangesDbToHistoryDb(exchangeRateToUsdCurrentRepository, exchangeRateToUsdHistoryRepository, mapper);
                    currencyService.DeleteCurrentExchRatesFromDb(exchangeRateToUsdCurrentRepository);
                    currencyService.PutCurrentExchangeRatesToDb(
                        exchangeRateToUsdCurrentRepository,
                        currencyService.GetExchangeRates());
                    Console.Write($"Current exchanges update for History DB at {currentDate}");
                    Thread.Sleep(3 * 59 * 1000); //  59 - because updating exchanges takes ~ 1 second and timer gets a displacement.
                }
                currentDate = DateTime.Now;
                currentDate = currentDate.AddSeconds(-currentDate.Second);
            }
        }
    }
}