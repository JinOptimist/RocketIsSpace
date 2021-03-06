namespace Rocket2.AdditionStructure
{
    public interface IAdditionStructure : IRocket
    {
        double Mass { get; }
        bool IsOpen { get; }

        void MakeClosed();

        void MakeOpened();

        string GetInfo();
    }
}