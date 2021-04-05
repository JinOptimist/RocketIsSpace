using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private ProfileRepository _profileRepository;

        public BankController(ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public IActionResult Bank()
        {
            var input = new RegistrationViewModel();
           
            return View(input);
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = new ProfileViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Login(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Bio = model.UserName + model.Password;
            return View(model);
        }
        public IActionResult Home()
        {
            var model = new RocketPreviewViewModel()
            {
                Name = "Soyus",
                Url = "image/Client/start.jpg"
            };

            return View(model);
        }
        public IActionResult Contacts()
        {
            var input = new ContactsVieModel()
            {
                PhoneNumber = "+375291191293",
                Email = "alesya.lis.1@mail.ru",
                PostAddress = "Belarus, Minsk, Timeriazeva 67"
            };
            return View(input);
        }
        [HttpGet]
        public IActionResult UserProfile()
        {
            var user = new UserProfileViewModel();

            return View(user);
        }

        [HttpPost]
        public IActionResult UserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userprofile = new Profile()
            {
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                PhoneNumber = model.PhoneNumber,
                PostAddress = model.PostAddress,
                IdentificationPassport = model.IdentificationPassport

            };

            _profileRepository.Save(userprofile);
            
            return RedirectToAction("UserProfileDataOutput");
        }
        public IActionResult UserProfileDataOutput()
        {
            var profileDateOutput = _profileRepository.GetAll()
                .Select(x => new UserProfileViewModel()
                {
                    Name = x.Name,
                    Sex = x.Sex,
                    BirthDate = x.BirthDate

                })
                .ToList();

            return View(profileDateOutput);
        }


    }
}
