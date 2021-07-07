using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.Models.Chart;
using SpaceWeb.Service;
using System.Linq;
using System.Text.Json;
using MazeCore;
using AutoMapper;
using SpaceWeb.Models.Maze;

namespace SpaceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _userService;
        private MazeBuilder _mazeBuilder;
        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            UserService userService,
            MazeBuilder mazeBuilder, 
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mazeBuilder = mazeBuilder;
            _mapper = mapper;
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

        public IActionResult Maze()
        {
            var mazeLevel = _mazeBuilder.Build(seed: 50);
            var viewModel = _mapper.Map<MazeViewModel>(mazeLevel);
            return View(viewModel);
        }
    }
}
