namespace BankSputink.Rocket
{
    public interface ISputnik
    {
        bool IsReadyToLaunch { get; set; }
        int Mass { get; }
        string Name { get; }

        string Launch();
    }
}