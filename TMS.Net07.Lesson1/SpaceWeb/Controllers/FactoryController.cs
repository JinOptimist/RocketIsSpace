using System;
using System.Collections.Generic;
using System.Linq;
using HumansResources.Humans.Persons;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models;

namespace SpaceWeb.Controllers
{
    public class FactoryController : Controller
    {
        public static List<RocketProfileViewModel> RocketUsers
            = new List<RocketProfileViewModel>();
        public IActionResult MainPage()
        {
            return View();
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
    }
}