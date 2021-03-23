using Microsoft.AspNetCore.Mvc;

namespace SpaceWeb.Controllers
{
    public class FactoryController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}