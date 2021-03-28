using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class MechanicController : Controller
    {
        public IActionResult MechanicPage()
        {
            return View();
        }
    }
}
