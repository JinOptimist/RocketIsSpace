using AutoMapper;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.Presentation
{
    public class RocketMechanicPresentation : IRocketMechanicPresentation
    {
        IRocketStageRepository _rocketStageRepository;
        IMapper _mapper;

        public RocketMechanicPresentation(IRocketStageRepository rocketStageRepository, IMapper mapper)
        {
            _rocketStageRepository = rocketStageRepository;
            _mapper = mapper;
        }

        public List<RocketStageAddViewModel> GetIndexViewModel() 
        {
            var models = _rocketStageRepository
                .GetAll()
                .Select(dbModel => _mapper.Map<RocketStageAddViewModel>(dbModel))
                .ToList();
            return models;
        }

        public RocketStageAddViewModel GetRocketStageAddViewModel(long id = 0) 
        {
            var rocketStage = _rocketStageRepository.Get(id);
            var viewModel = _mapper.Map<RocketStageAddViewModel>(rocketStage)
                ?? new RocketStageAddViewModel();

            return viewModel;
        }
    }
}
