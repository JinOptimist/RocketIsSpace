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
    }
}
