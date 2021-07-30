using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Service
{
    public interface ITransactionService
    {
        string Deposit(BankAccount toAccount, decimal amount);
        void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal transferAmount);
        void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal transferAmount, Currency transferCurrency);
        void Transfer(long fromAccountId, long toAccountId, decimal transferAmount);
        void Transfer(long fromAccountId, long toAccountId, decimal transferAmount, Currency transferCurrency);
        string Withdrawal(BankAccount fromAccount, decimal amount);
    }
}