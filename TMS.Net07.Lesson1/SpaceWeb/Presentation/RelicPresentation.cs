using AutoMapper;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Presentation
{
    public class RelicPresentation : IRelicPresentation
    {
        private IRelicRepository _relicRepository;
        private IMapper _mapper;

        public RelicPresentation(IRelicRepository relicRepository, IMapper mapper)
        {
            _relicRepository = relicRepository;
            _mapper = mapper;
        }

        public List<RelicViewModel> GetIndexViewModels()
        {
            var models = _relicRepository
                .GetAll()
                .Where(x => x.Price > 0)
                .Select(dbModel =>
                    _mapper.Map<RelicViewModel>(dbModel)
                )
                .ToList();
            return models;
        }
    }
}
