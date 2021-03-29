using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class UserController : Controller
    {
        //Это плохо. Удалить как только добавим БД
        //public static List<ProfileViewModel> Users
        //    = new List<ProfileViewModel>();

        private SpaceDbContext _dbContext;

        public static int Counter = 0;

        public UserController(SpaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

            var user = _dbContext.Users.
                SingleOrDefault(x => x.Name == model.Login);

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
                _dbContext.Users.All(user => user.Name != model.Login);
            if (isUserUniq)
            {
                var user = new User()
                {
                    Name = model.Login,
                    Password = model.Password,
                    Age = 18
                };
                _dbContext.Users.Add(user);
                
                _dbContext.SaveChanges();
            }

            return View(model);
        }

        public JsonResult IsUserExist(string name)
        {
            Thread.Sleep(3000);
            var answer = _dbContext.Users.Any(x => x.Name == name);
            return Json(answer);
        }
    }
}
