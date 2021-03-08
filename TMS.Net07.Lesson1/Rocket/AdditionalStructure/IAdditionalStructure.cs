using Rocket.RocketFactory;

namespace Rocket.AdditionalStructure
{
    public interface IAdditionalStructure : IRocket
    {
        double Weight { get; set; }
        bool IsOpen { get; }

        void MakeClosed();

        void MakeOpened();

        string GetInfo();
    }
}