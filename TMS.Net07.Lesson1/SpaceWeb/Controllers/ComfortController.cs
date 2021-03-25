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
    }
}
