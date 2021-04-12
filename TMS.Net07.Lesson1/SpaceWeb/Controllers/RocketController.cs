using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        private RocketProfileRepository _rocketProfileRepository;
        private ComfortRepository _comfortRepository;
        private AdditionRepository _additionRepository;
        public RocketController(RocketProfileRepository rocketProfileRepository,
            ComfortRepository comfortRepository,
            AdditionRepository additionRepository)
        {
            _rocketProfileRepository = rocketProfileRepository;
            _comfortRepository = comfortRepository;
            _additionRepository = additionRepository;
        }

        [HttpGet]
        public IActionResult ComfortPage()
        {
            var model = new ComfortFormViewModel();
            return View("Comfort/ComfortPage", model);
        }

        [HttpPost]
        public IActionResult ComfortPage(ComfortFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Comfort/ComfortPage", viewModel);
            }

            var comfort = new Comfort()
            {
                ToiletCount = viewModel.ToiletCount,
                KitchenSeatsCount = viewModel.KitchenSeatsCount,
                StorageCapacity = viewModel.StorageCapacity,
                SleepingCapsulesCount = viewModel.SleepingCapsulesCount
            };

            _comfortRepository.Save(comfort);
            return RedirectToAction("ComfortPage");
        }

        // [HttpGet]
        // public IActionResult Login()
        // {
        //     var model = new RocketLoginViewModel();
        //     return View(model);
        // }
        //
        // [HttpPost]
        // public IActionResult Login(RocketLoginViewModel model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(model);
        //     }
        //
        //     var user = RocketUsers
        //         .SingleOrDefault(x => x.UserName == model.UserName);
        //
        //     if (user == null)
        //     {
        //         ModelState.AddModelError(
        //             nameof(RocketLoginViewModel.Login),
        //             "Нет такого пользователя");
        //         return View(model);
        //     }
        //
        //     if (user.Password != model.Password)
        //     {
        //         ModelState.AddModelError(
        //             nameof(RegistrationViewModel.Password),
        //             "Не правильный праоль");
        //         return View(model);
        //     }
        //
        //     return RedirectToAction("Profile", "User");
        // }
        public IActionResult MainPage()
        {
            return View("Factory/MainPage");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RocketRegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(RocketRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUserUniq = _rocketProfileRepository.GetByName(model.UserName) == null;
            if (isUserUniq)
            {
                var user = new RocketProfile
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.LastName,
                    BirthDate = model.DateOfBirth,
                    UserName = model.UserName,
                    Password = model.Password
                };
                _rocketProfileRepository.Save(user);
            }

            return View(model);
        }

        // public JsonResult IsUserExist(string name)
        // {
        //     var answer = RocketUsers.Any(x => x.UserName == name);
        //     return Json(answer);
        // }

        public IActionResult ToiletPage()
        {
            return View("Comfort/ToiletPage");
        }

        public IActionResult KitchenPage()
        {
            return View("Comfort/KitchenPage");
        }

        public IActionResult CCenterPage()
        {
            return View("Comfort/CCenterPage");
        }

        public IActionResult CapsulePage()
        {
            return View("Comfort/CapsulePage");
        }

        public IActionResult Rocket()
        {
            return View("OriginRocket/Rocket");
        }

        public IActionResult RocketShop()
        {
            return View("OriginRocket/RocketShop");
        }

        [HttpGet]
        public IActionResult AdditionPage()
        {
            var model = new AdditionFormViewModel();
            return View("Addition/AdditionPage", model);
        }

        [HttpPost]
        public IActionResult AdditionPage(AdditionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Addition/AdditionPage", model);
            }

            var addition = new Addition()
            {
                RescueCapsuleCount = model.RescueCapsuleCount,
                RestRoomCount = model.RestRoomCount,
                Id = model.Id,
                BotanicalCenterCount = model.BotanicalCenterCount,
                ObservarionDeckCount = model.ObservarionDeckCount
            };

            _additionRepository.Save(addition);
            return View("Comfort/ComfortPage");
        }
    }
}
