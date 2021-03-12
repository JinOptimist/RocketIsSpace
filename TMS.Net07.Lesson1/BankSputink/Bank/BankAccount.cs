using System;
using System.Collections.Generic;
using System.Text;

namespace BankSputink.Bank
{
    class BankAccount
    {
        private static int accountNumberSeed = 1234567890;

        private List<Transaction> allTransactions = new List<Transaction>();
        public string Number { get; }           //номер счета
        public Client Owner { get; }            //клиент, владелец счета
        public DateTime СreationDate { get; private set; }

        private bool _isFrozen;                 //проверка, является ли счёт замороженным/заблокированным
        //валюта
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;     //баланс расчитывается как сумма транзакций
                }
                return balance;     //может лучше создать поле, в которое 
            }

        }
        public BankAccount(Client owner /*валюта*/)
        {
            Number = accountNumberSeed.ToString();  //присваивается номер аккаунта/счета
            accountNumberSeed++;                    //номер следующего аккаунта/счета увеличивается на 1
            //this.валюта = валюта;
            СreationDate = DateTime.Now;            //присваивается дата создания
            Owner = owner;                          //аккаунту присваивается владелец
        }
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }
        public void Transfer ()
        {
        }
        public void FreezeAccount()
        {
            if (_isFrozen)
            {
                throw new Exception("Bank account is already frozen.");
            }
            _isFrozen = true;            
        }
        public void UnfreezeAccount()
        {
            if (_isFrozen)
            {
                _isFrozen = false;
            }
            throw new Exception("Bank account is already enabled.");
        }
        public void GetListOfTransactions()
        {
        }
    }
}
