using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        private RocketProfileRepository _rocketProfileRepository;
        private ComfortRepository _comfortRepository;
        private OrderRepository _orderRepository;
        private IMapper _mapper;
        private AdditionRepository _additionRepository;
        private RocketService _rocketService;
        public RocketController(RocketProfileRepository rocketProfileRepository,
            ComfortRepository comfortRepository, IMapper mapper, OrderRepository orderRepository,
        AdditionRepository additionRepository, RocketService rocketService)
        {
            _rocketProfileRepository = rocketProfileRepository;
            _comfortRepository = comfortRepository;
            _mapper = mapper;
            _additionRepository = additionRepository;
            _rocketService = rocketService;
            _orderRepository = orderRepository;
        }
        
        [Authorize]
        public IActionResult Profile()
        {
            var user = _rocketService.GetCurrent();
            var viewModel = _mapper.Map<RocketProfileViewModel>(user);
            return View("Profile",viewModel);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult ComfortPage()
        {
            var model = new ComfortFormViewModel();
            return View("Comfort/ComfortPage", model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult ComfortPage(ComfortFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Comfort/ComfortPage", viewModel);
            }

            var comfort = _mapper.Map<Comfort>(viewModel);

            _comfortRepository.Save(comfort);
            return RedirectToAction("ComfortPage");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new RocketLoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(RocketLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _rocketProfileRepository.GetByName(model.UserName);

            if (user == null)
            {
                return View(model);
            }

            if (user.Password != model.Password)
            {
                return View(model);
            }
            
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.RocketAuthMethod));
            var claimsIdentity = new ClaimsIdentity(claims, Startup.RocketAuthMethod);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Profile", "Rocket");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("MainPage", "Rocket");
        }

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
                var user = _mapper.Map<RocketProfile>(model);
                _rocketProfileRepository.Save(user);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangeName()
        {
            var user = _rocketService.GetCurrent();
            var model = new ChangeNameViewModel()
            {
                Id=user.Id,
                OldName = user.Name
            };

            return View("Factory/ChangeName",model);
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult ChangeName(ChangeNameViewModel viewModel)
        {
            var user = _rocketService.GetCurrent();
            user.Name = viewModel.NewName;
            _rocketProfileRepository.Save(user);
            return RedirectToAction("Profile","Rocket");
        }
        
        // public JsonResult IsUserExist(string name)
        // {
        //     var answer = RocketUsers.Any(x => x.UserName == name);
        //     return Json(answer);
        // }
        [Authorize]
        public IActionResult ToiletPage()
        {
            return View("Comfort/ToiletPage");
        }
        [Authorize]
        public IActionResult KitchenPage()
        {
            return View("Comfort/KitchenPage");
        }
        [Authorize]
        public IActionResult CCenterPage()
        {
            return View("Comfort/CCenterPage");
        }
        [Authorize]
        public IActionResult CapsulePage()
        {
            return View("Comfort/CapsulePage");
        }
        [Authorize]
        public IActionResult Rocket()
        {
            return View("OriginRocket/Rocket");
        }
        [HttpGet]
        [Authorize]
        public IActionResult RocketShop()
        {
            var order = new OrderViewModel();
            return View("OriginRocket/RocketShop",order);
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult RocketShop(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("OriginRocket/RocketShop",orderViewModel);
            }

            var order = _mapper.Map<Order>(orderViewModel);
            _orderRepository.Save(order);
            
            return View("OriginRocket/RocketShop",orderViewModel);

        }

        [HttpGet]
        [Authorize]
        public IActionResult AdditionPage()
        {
            var model = new AdditionFormViewModel();
            return View("Addition/AdditionPage", model);
        }

        [HttpPost]
        [Authorize]
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
