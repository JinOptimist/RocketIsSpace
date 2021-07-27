namespace SpaceWeb.Service
{
    public interface IGenerationService
    {
        string GenerateAccountNumber(int accountNumberLength = 10);
        string GenerateTransactionNumber();
    }
}