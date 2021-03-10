namespace Rocket2.ComfortStructure
{
    public interface IComfortStructure : IRocket
    {
        double Mass { get; }
        string GetInfo();
    }
}