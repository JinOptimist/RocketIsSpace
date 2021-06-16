using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IExchangeRateToUsdCurrentRepository : IBaseRepository<ExchangeRateToUsdCurrent>
    {
        ExchangeRateToUsdCurrent GetExchangeRate(Currency currency, TypeOfExchange type);
    }
}