using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ExchangeRateToUsdCurrentRepository : BaseRepository<ExchangeRateToUsdCurrent>
    {
        public ExchangeRateToUsdCurrentRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }

        public ExchangeRateToUsdCurrent GetExchangeRate(Currency currency, TypeOfExchange type)
        {
            return _dbSet.SingleOrDefault(x =>
                x.Currency == currency
                && x.TypeOfExch == type);
        }
    }
}
