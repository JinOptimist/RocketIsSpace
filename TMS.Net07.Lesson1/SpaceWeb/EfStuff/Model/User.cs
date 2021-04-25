using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public DateTime BirthDate { get; set; }
        
        public string Email { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }

        public string AvatarUrl { get; set; }

        public JobType JobType { get; set; }

        public virtual List<Rocket> MyRockets { get; set; }

        public virtual List<Rocket> TestedRockets { get; set; } 

        public virtual Rocket MyFavouriteRocket { get; set; }

        public virtual List<BankAccount> BankAccounts { get; set; }
       
        public virtual Profile Profile { get; set; }
    }
}
