using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class MechanicController : Controller
    {
        [HttpGet]
        public IActionResult MechanicPage()
        {
            var model = new MechanicViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MechanicPage(MechanicViewModel mechanicViewModel)
        {
            return View(mechanicViewModel);
        }
    }
}
