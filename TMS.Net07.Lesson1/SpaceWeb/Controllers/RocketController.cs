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
        public IActionResult Login()
        {
            var model = new RocketLoginViewModel();
            return View(model);
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
            var model = new FactoryRegistrationViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Registration(FactoryRegistrationViewModel model)
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
