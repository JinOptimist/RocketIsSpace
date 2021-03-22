using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Client()
        {
            var input = new InputViewModel()
            {
                Name = "Alesya"

            };
            return View(input);
        }
    }
}
