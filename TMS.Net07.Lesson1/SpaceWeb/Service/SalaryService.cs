using SpaceWeb.EfStuff.CustomException;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Model.Enum;
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
        private IAccrualRepository _accrualRepository;
        private IPaymentRepository _paymentRepository;
        private IBankAccountRepository _bankAccountRepository;
        private IEmployeRepository _employeRepository;
        private const int HOURS_IN_DAY = 24;


        public SalaryService(
            IAccrualRepository accrualRepository,
            IPaymentRepository paymentRepository,
            IBankAccountRepository bankAccountRepository, 
            IEmployeRepository employeRepository)
        {
            _accrualRepository = accrualRepository;
            _paymentRepository = paymentRepository;
            _bankAccountRepository = bankAccountRepository;
            _employeRepository = employeRepository;
        }

        private int CalculateHours(int hourStartWorking, int hourEndWorking)
        {

            var result = hourEndWorking - hourStartWorking;
            return result < 0 ? HOURS_IN_DAY + result : result;
        }

        public decimal CalculateAccrual(DateTime date, Employe employe)
        {
            int days;
            if (date.Month == employe.InviteDate.Value.Month
                && date.Year == employe.InviteDate.Value.Year)
            {
                var endPeriod = new DateTime(employe.InviteDate.Value.AddMonths(1).Year, employe.InviteDate.Value.AddMonths(1).Month, 1);
                days = employe.InviteDate.Value.GetWorkingDaysInPeriod(endPeriod);
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

        public Accrual GetAccrualInAMonth(long employeId, DateTime date) =>
            _accrualRepository
                .GetExist(employeId, date);

        public List<Accrual> GetAllAccruals(long employeId) =>
            _accrualRepository
                .GetAllAccruals(employeId);

        public List<Payment> GetAllPayments(long employeId) =>
            _paymentRepository
                .GetAllPayments(employeId);

        public bool Pay(PaymentViewModel paymentViewModel)
        {
            //from
            var departmentId = _employeRepository.Get(paymentViewModel.EmployeId).Department.Id;
            var accountFrom = _bankAccountRepository.GetDepartmentAccounts(departmentId).FirstOrDefault();

            if (accountFrom == null)
                throw new BankAccountException();

            //to
            var accountTo = _bankAccountRepository.GetSpecifiedAccountByEmploye(paymentViewModel.EmployeId, BankAccountType.Salary);

            var transferResponse = _bankAccountRepository.Transfer(accountFrom.Id, accountTo.Id, paymentViewModel.Amount);

            if(transferResponse)
            {
                var payment = new Payment()
                {
                    Employe = _employeRepository.Get(paymentViewModel.EmployeId),
                    Date = paymentViewModel.Date,
                    Amount = paymentViewModel.Amount,
                    BankAccount = accountTo
                };
                _paymentRepository.Save(payment);
            }

            return transferResponse;
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

        public decimal GetIndebtedness(long employeId) =>
            GetAllAccruals(employeId)
                .Select(x => x.Amount)
                .Sum()
            - GetAllPayments(employeId)
                .Select(x => x.Amount)
                .Sum();

        public decimal GetPayedSalary(long employeId) =>
            GetAllPayments(employeId)
                .Select(x => x.Amount)
                .Sum();
    }
}