namespace Rocket.AdditionStructure
{
    public interface IAdditionStructure : IRocket
    {
        double Mass { get; set; }
        bool IsOpen { get; }

        void MakeClosed();

        void MakeOpened();

        string GetInfo();
    }
}