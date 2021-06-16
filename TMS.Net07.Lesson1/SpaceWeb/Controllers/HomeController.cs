using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.Models.Chart;
using SpaceWeb.Service;
using System.Linq;
using System.Text.Json;

namespace SpaceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _userService;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Smile");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccountChartInfo()
        {
            var user = _userService?.GetCurrent();

            //проект не грузится, если юзер не залогинен

            if (user!=null)
            {
                var currencies = user.BankAccounts.Select(x => x.Currency).Distinct();

                var chartViewModel = new ChartViewModel();
                chartViewModel.Labels = currencies.Select(x => x.ToString()).ToList();
                var datasetViewModel = new DatasetViewModel()
                {
                    Label = "Валюты"
                };
                datasetViewModel.Data =
                    currencies.Select(c =>
                        user.BankAccounts
                            .Where(b => b.Currency == c)
                            .Select(b => b.Amount)
                            .Sum())
                    .ToList();

                chartViewModel.Datasets.Add(datasetViewModel);

                return Json(chartViewModel);
            }
            return Json("null user");
        }
    }
}
