using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models.Chart;
using SpaceWeb.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HumanApiController : ControllerBase
    {
        private IHumanPresentation _humanPresentation;

        public HumanApiController(IHumanPresentation humanPresentation)
        {
            _humanPresentation = humanPresentation;
        }

        public MyChartViewModel<int> GetGraph()
        {
            return _humanPresentation.GetChartForWorkersInDepartment();
        }

        public decimal GetAmmount(long userId)
        {
            return 100;
        }
    }
}
