using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ExchangeRateToUsdHistoryRepository : BaseRepository<ExchangeRateToUsdHistory>
    {
        public ExchangeRateToUsdHistoryRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}
