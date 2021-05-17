using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class BanksCardViewModel
    {
        public long Id { get; set; }
        public string Card { get; set; }
        public User Owner { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
