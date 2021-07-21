using AutoMapper;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Presentation
{
    public class BankCardPresentation : IBankCardPresentation
    {
        private BanksCardRepository _bankCardRepository;
        private IMapper _mapper;

        public BankCardPresentation(BanksCardRepository bankCardRepository, IMapper mapper)
        {
            _bankCardRepository = bankCardRepository;
            _mapper = mapper;
        }

        public List<BanksCardViewModel> GetIndexViewModels()
        {
            var models = _bankCardRepository
                .GetAll()
                .Where(x => x.Count > 0)
                .Select(dbModel =>
                    _mapper.Map<BanksCardViewModel>(dbModel)
                )
                .ToList();

            return models;
        }
    }
}
