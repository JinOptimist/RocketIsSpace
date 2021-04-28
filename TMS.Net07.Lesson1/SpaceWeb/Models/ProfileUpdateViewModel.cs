using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class ProfileUpdateViewModel
    {
        public IFormFile Avatar { get; set; }

        public string Email { get; set; }
    }
}
