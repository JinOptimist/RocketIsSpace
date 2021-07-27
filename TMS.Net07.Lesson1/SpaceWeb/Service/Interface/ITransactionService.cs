using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Service
{
    public interface ITransactionService
    {
        void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal transferAmount);
        void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal transferAmount, Currency transferCurrency);
        void Transfer(long fromAccountId, long toAccountId, decimal transferAmount);
        void Transfer(long fromAccountId, long toAccountId, decimal transferAmount, Currency transferCurrency);
    }
}