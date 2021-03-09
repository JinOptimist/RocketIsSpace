using System;



namespace BankSputink.Rocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("Alesya", "Lis", "Aaaa", new DateTime(1993,12,19));
            client.PrintClient();
        }
    }
}
