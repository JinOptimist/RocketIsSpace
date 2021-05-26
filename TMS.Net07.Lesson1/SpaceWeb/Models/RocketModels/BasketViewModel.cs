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
        
        public Currency currentCurrency { get; set; }
        public BasketViewModel(List<BankAccount> accounts)
        {
            BankAccounts = accounts;
            BAOptions = new List<SelectListItem>();
            BAOptions.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "-1",
                Disabled = true,
                Selected=true
            });
            foreach (var option in BankAccounts)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = option.AccountNumber,
                    Value = option.AccountNumber
                };
                BAOptions.Add(selectListItem);
            }

            currentCurrency = Currency.USD;
        }
    }
}
