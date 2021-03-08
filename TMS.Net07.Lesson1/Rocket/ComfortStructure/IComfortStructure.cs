namespace Rocket.ComfortStructure
{
    public interface IComfortStructure : IRocket
    {
        double Mass { get; }
        string GetInfo();
    }
}