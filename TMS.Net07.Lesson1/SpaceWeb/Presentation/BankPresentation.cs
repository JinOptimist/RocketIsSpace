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
    public class BankPresentation : IBankPresentation
    {
        private IProfileRepository _profileRepository;
        private IMapper _mapper;

        public BankPresentation(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public QuestionaryViewModel GetProfileViewModel(long id)
        {
            var userprofile = _profileRepository.Get(id);
            var profile = _mapper.Map<QuestionaryViewModel>(userprofile)
                ?? new QuestionaryViewModel();
            return profile;
        }
    }
}
