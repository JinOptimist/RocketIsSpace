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
            var model = new ProfileViewModel()
            {
                Name = "Иван",
                DateRegistration = new DateTime(2000, 1, 10)
            };
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
