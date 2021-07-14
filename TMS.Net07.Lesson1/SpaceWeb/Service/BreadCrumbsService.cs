using Microsoft.AspNetCore.Http;
using SpaceWeb.Controllers;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Service
{
    public class BreadCrumbsService
    {
        public List<ControllerAndParrent> AllControllers { get; set; }
            = new List<ControllerAndParrent>();

        public BreadCrumbsService()
        {
            AddController(typeof(HomeController), null);
            AddController(typeof(BankController), typeof(HomeController));
            AddController(typeof(RocketController), typeof(HomeController));

            AddController(typeof(UserController), typeof(HomeController));
        }

        private void AddController(Type controller, Type parent)
        {
            var controllerName = controller.Name.Replace("Controller", "");
            var parentName = parent?.Name.Replace("Controller", "");
            AllControllers.Add(new ControllerAndParrent()
            {
                CurrentControllerTypeName = controllerName,
                ParentControllerTypeName = parentName
            });
        }

        public List<BreadCrumbViewModel> GetBreadCrumbs(
            HttpContext httpContext)
        {
            var controller = httpContext.Request.RouteValues["controller"] as string;
            var action = httpContext.Request.RouteValues["action"] as string;
            return Recurtion(controller, action);
        }

        private List<BreadCrumbViewModel> Recurtion(string controller, string action)
        {
            var oneStep = AllControllers
                .Single(x => x.CurrentControllerTypeName == controller);

            var breadCrumbs = oneStep.ParentControllerTypeName == null
                ? new List<BreadCrumbViewModel>()
                : Recurtion(oneStep.ParentControllerTypeName, "");

            breadCrumbs.Add(new BreadCrumbViewModel
            {
                Title = controller,
                Url = GenerateUrl(controller, action)
            });

            return breadCrumbs;
        }

        private string GenerateUrl(string controller, string action)
        {
            return $"/{controller}/{action}";
        }
    }

    public class ControllerAndParrent
    {
        public string CurrentControllerTypeName { get; set; }

        public string ParentControllerTypeName { get; set; }
    }
}

