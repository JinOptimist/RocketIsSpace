using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.Models;
using SpaceWeb.Models.RocketModels;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            var model = new RocketLoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(RocketLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = RocketUsers
                .SingleOrDefault(x => x.UserName == model.UserName);

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
        
        public static List<RocketProfileViewModel> RocketUsers
            = new List<RocketProfileViewModel>();
        public IActionResult MainPage()
        {
            return View("Factory/MainPage");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RocketRegistrationViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Registration(RocketRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var isUserUniq =
                RocketUsers.All(user => user.UserName != model.UserName);
            if (isUserUniq)
            {
                RocketUsers.Add(new RocketProfileViewModel(model));
            }
            return View(model);
        }
        
        public JsonResult IsUserExist(string name)
        {
            var answer = RocketUsers.Any(x => x.UserName == name);
            return Json(answer);
        }
        public IActionResult ComfortPage()
        {
            return View("Comfort/ComfortPage");
        }

        public IActionResult ToiletPage()
        {
            return View("Comfort/ToiletPage");
        }

        public IActionResult KitchenPage()
        {
            return View("Comfort/KitchenPage");
        }

        public IActionResult CCenterPage()
        {
            return View("Comfort/CCenterPage");
        }

        public IActionResult CapsulePage()
        {
            return View("Comfort/CapsulePage");
        }
    }
}
