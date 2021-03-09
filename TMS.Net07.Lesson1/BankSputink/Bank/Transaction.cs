using System;
using System.Collections.Generic;
using System.Text;


namespace TMS.Net07.Rocket.Bank
{
    class Transaction
    {
        
        public decimal Amount { get; }
        public DateTime Date { get; }
        public Transaction(decimal amount, DateTime date)
        {
            this.Amount = amount;
            this.Date = date;   
        }
    }
}
