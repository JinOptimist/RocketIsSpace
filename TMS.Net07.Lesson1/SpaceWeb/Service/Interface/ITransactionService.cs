namespace SpaceWeb.Service
{
    public interface ITransactionService
    {
        void Transfer(decimal transferAmount, long transferToId);
        bool TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount);
    }
}