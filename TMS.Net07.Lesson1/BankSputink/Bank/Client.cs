using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Net07.Rocket.Bank
{
    class Client
    {
        private string _lastName;
        private string _firstName;
        private string _middletName;
        private DateTime _birthday;
        public Client(string lastName, string firstName, string middletName, DateTime birthday)
        {
            _lastName = lastName;
            _firstName = firstName;
            _middletName = middletName;
            _birthday = birthday;
        }
        public void PrintClient()
        {
            Console.WriteLine($"Name: {_firstName}\n Last name : {_lastName}\n Middle name : {_middletName}\n Birthday : {_birthday}");
        }
    }
}
