using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class MechanicViewModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
