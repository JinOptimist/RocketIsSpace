using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models;

namespace SpaceWeb.Controllers
{
    public class HumanController : Controller
    {
        
        [HttpGet]
        public IActionResult Person()
        {
            var model = new PersonViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Person(PersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return View(model);
        }
    }
}
