using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class UserController : Controller
    {
        //Это плохо. Удалить как только добавим БД
        public static List<ProfileViewModel> Users 
            = new List<ProfileViewModel>();

        public static int Counter = 0;

        public IActionResult Profile()
        {
            var model = new List<RocketPreviewViewModel>();

            model.Add(new RocketPreviewViewModel()
            {
                Name = "Союз",
                Url = "/image/R1.jpeg"
            });

            model.Add(new RocketPreviewViewModel()
            {
                Name = "Протон",
                Url = "/image/R2.jpg"
            });

            model.Add(new RocketPreviewViewModel()
            {
                Name = "Солют",
                Url = "/image/R3.jpg"
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Users
                .SingleOrDefault(x => x.UserName == model.Login);

            if (user == null)
            {
                ModelState.AddModelError(
                    nameof(RegistrationViewModel.Login),
                    "Нет такого пользователя");
                return View(model);
            }
            
            if (user.Password != model.Password)
            {
                ModelState.AddModelError(
                    nameof(RegistrationViewModel.Password), 
                    "Не правильный праоль");
                return View(model);
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Старый способ.
            //var isUserUniq = true;
            //foreach (var user in Users)
            //{
            //    if (user.UserName == model.Login)
            //    {
            //        isUserUniq = false;
            //    }
            //}

            //Новый способ LINQ
            var isUserUniq = 
                Users.All(user => user.UserName != model.Login);
            if (isUserUniq)
            {
                Users.Add(new ProfileViewModel()
                {
                    DateRegistration = DateTime.Now,
                    UserName = model.Login,
                    Password = model.Password
                });
            }

            return View(model);
        }

    }
}
