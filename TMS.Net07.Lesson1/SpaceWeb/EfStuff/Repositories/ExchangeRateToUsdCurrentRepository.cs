using SpaceWeb.EfStuff.Model;
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
    }
}
