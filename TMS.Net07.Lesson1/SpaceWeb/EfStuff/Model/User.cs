﻿using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public virtual List<Rocket> MyRockets { get; set; }

        public virtual List<Rocket> TestedRockets { get; set; } 

        public virtual Rocket MyFavouriteRocket { get; set; }

        public long? ClientForeignKey { get; set; }
        public virtual Client Client { get; set; }

        public long? EmployeForeignKey { get; set; }
        public virtual Employe Employe { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string PostAddress { get; set; }
    }
}
