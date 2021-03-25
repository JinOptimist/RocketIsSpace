using System;
using System.Collections.Generic;
using System.Text;


namespace BankSputink.Bank
{
    class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Note { get; }
        
        //валюта

        public Transaction(decimal amount, DateTime date, string note /*валюта*/)
        {
            Amount = amount;
            Date = date;
            Note = note;
            //валюта
        }
    }
}
