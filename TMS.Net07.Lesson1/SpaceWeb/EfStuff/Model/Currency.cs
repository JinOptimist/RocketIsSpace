using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public enum Currency
    {
        [Display(Name = "BYN")]
        BYN = 1,
        [Display(Name = "USD")]
        USD = 2,
        [Display(Name = "EUR")]
        EUR = 3,
        [Display(Name = "PLN")]
        PLN = 4,
        [Display(Name = "GBP")]
        GBP = 5
    }
}
