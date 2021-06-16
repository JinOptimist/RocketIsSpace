using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Bank
{
    public class CurrencyExchengerViewModel
    {
        public CurrencyExchengerViewModel()
        {
            CurrencyFromDropFill();
            CurrencyToDropFill();
        }
        public Currency CurrencyFrom { get; set; }
        public Currency CurrencyTo { get; set; }
        public List<SelectListItem> CurrencyFromDrop { get; set; }
        public List<SelectListItem> CurrencyToDrop { get; set; }

        public void CurrencyFromDropFill()
        {
            CurrencyFromDrop = new List<SelectListItem>();

            var currency = Enum.GetValues(typeof(Currency));

            for (int i = 0; i < currency.Length; i++)
            {
                var option = new SelectListItem();
                option.Value = ((int)currency.GetValue(i)).ToString();
                option.Text = currency.GetValue(i).ToString();
                CurrencyFromDrop.Add(option);
            }
        }
        public void CurrencyToDropFill()
        {
            CurrencyToDrop = new List<SelectListItem>();

            var currency = Enum.GetValues(typeof(Currency));

            for (int i = 0; i < currency.Length; i++)
            {
                var option = new SelectListItem();
                option.Value = ((int)currency.GetValue(i)).ToString();
                option.Text = currency.GetValue(i).ToString();
                CurrencyToDrop.Add(option);
            }
        }

    }
}



