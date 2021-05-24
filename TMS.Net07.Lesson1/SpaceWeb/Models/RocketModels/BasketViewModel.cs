using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.RocketModels
{
    public class BasketViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public List<SelectListItem> BAOptions { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public BasketViewModel(List<BankAccount> accounts)
        {
            BankAccounts = accounts;
            BAOptions = new List<SelectListItem>();
            foreach (var option in BankAccounts)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = option.AccountNumber,
                    Value = option.Id.ToString()
                };
                BAOptions.Add(selectListItem);
            }
        }
    }
}
