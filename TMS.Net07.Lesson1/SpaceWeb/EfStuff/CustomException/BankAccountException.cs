using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.CustomException
{
    public class BankAccountException : Exception
    {
        public override string Message => "Account do not exist";
    }
}
