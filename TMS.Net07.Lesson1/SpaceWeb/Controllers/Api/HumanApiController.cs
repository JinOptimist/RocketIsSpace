using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models.Chart;
using SpaceWeb.Models.Human;
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

        public List<ShortEmployeViewModel> UpdateEmployes(long departmentId)
        {
            return _humanPresentation.UpdateEmployes(departmentId);
        }

        public AccrualViewModel GetEmloyeAccrualsInfo(long employeId)
        {
            return _humanPresentation.GetAccrualViewModel(employeId);
        }

        public decimal ChangeDate(DateTime date, long employeId)
        {
            return _humanPresentation.CalculateAccrual(date, employeId);
        }

        public PaymentViewModel GetEmployePaymentInfo(long employeId)
        {
            return _humanPresentation.GetPaymentViewModel(employeId);
        }
    }
}
