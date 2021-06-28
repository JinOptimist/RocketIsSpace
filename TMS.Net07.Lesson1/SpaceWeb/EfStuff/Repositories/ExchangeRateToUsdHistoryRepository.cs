using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ExchangeRateToUsdHistoryRepository 
        : BaseRepository<ExchangeRateToUsdHistory>, IExchangeRateToUsdHistoryRepository
    {
        public ExchangeRateToUsdHistoryRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }

        public List<decimal> GetExchangeRateForChart(Currency currency, TypeOfExchange type)
        {
            return _dbSet
                .Where(x => x.TypeOfExch == type)
                .Where(x => x.Currency == currency)
                .Select(x => x.ExchRate)
                .ToList();
        }
    }
}
