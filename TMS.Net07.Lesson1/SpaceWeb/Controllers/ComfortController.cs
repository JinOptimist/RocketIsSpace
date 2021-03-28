using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class ComfortController : Controller
    {
        public IActionResult ComfortPage()
        {
            return View();
        }

        public IActionResult ToiletPage()
        {
            return View();
        }

        public IActionResult KitchenPage()
        {
            return View();
        }

        public IActionResult CCenterPage()
        {
            return View();
        }

        public IActionResult CapsulePage()
        {
            return View();
        }
    }
}
