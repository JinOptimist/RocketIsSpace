using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Extensions;
using SpaceWeb.Models.Human;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.Service
{
    public class SalaryService : ISalaryService
    {
        private IAccrualRepository _employeRepository;
        private const long TICKS_IN_ONE_SECOND = 10000000;
        private const long TICKS_IN_ONE_MINUTE = TICKS_IN_ONE_SECOND * 60;
        private const long TICKS_IN_ONE_HOUR = TICKS_IN_ONE_MINUTE * 60;


        public SalaryService(IAccrualRepository employeRepository)
        {
            _employeRepository = employeRepository;
        }

        private int CalculateHours(int hourStartWorking, int hourEndWorking)
        {
            var start = new DateTime(TICKS_IN_ONE_HOUR * hourStartWorking);
            var end = new DateTime(TICKS_IN_ONE_HOUR * hourEndWorking);
            var result = new DateTime();
            if (end < start)
                end = end.AddDays(1);
            while (start < end)
            {
                start = start.AddHours(1);
                result = result.AddHours(1);
            }
            return result.Hour;
        }

        public decimal CalculateAccrual(DateTime date, Employe employe)
        {
            int days;
            if (employe.EmployeStatus == EmployeStatus.Accepted 
                && 
                date.Month == employe.StatusDate.Month 
                && 
                date.Year == employe.StatusDate.Year)
            {
                var endPeriod = new DateTime(employe.StatusDate.AddMonths(1).Year, employe.StatusDate.AddMonths(1).Month, 1);
                days = employe.StatusDate.GetWorkingDaysInPeriod(endPeriod);             
            }
            else
            {
                var endPeriod = date.AddMonths(1);
                days = date.GetWorkingDaysInPeriod(endPeriod);
            }
            var startHour = employe.Department.HourStartWorking;
            var endHour = employe.Department.HourEndWorking;
            var hours = CalculateHours(startHour, endHour);
            var workingHours = hours * days;
            var salary = workingHours * employe.SalaryPerHour;
            return salary;
        }

        public Accrual GetAccrualInAMonth(long employeId, DateTime date)
        {
            return _employeRepository.GetExist(employeId, date);
        }

        public List<Accrual> GetAllAccruals(long EmployeId)
        {
            return _employeRepository.GetAllAccruals(EmployeId);
        }

        public void GetAllPayedAccruals()
        {
            throw new NotImplementedException();
        }

        public void GetPayedAccrualInAMonth()
        {
            throw new NotImplementedException();
        }

        public void PayAccrual()
        {
            throw new NotImplementedException();
        }

        public List<DateTime> PickUpMonths(DateTime start, DateTime end, List<DateTime> accruals)
        {
            List<DateTime> workPeriodMonths = new List<DateTime>();
            while (start <= end)
            {
                workPeriodMonths.Add(start);
                start = start.AddMonths(1);
            }
            return workPeriodMonths.Except(accruals).ToList();
        }

    }
}