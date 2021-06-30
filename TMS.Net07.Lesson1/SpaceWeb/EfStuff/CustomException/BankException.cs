using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.CustomException
{
    public class BankException : Exception
    {
        public override string Message => "Accaount ammount can't be negative";
    }
}
