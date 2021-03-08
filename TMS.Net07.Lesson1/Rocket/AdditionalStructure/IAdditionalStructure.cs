using Rocket.RocketFactory;

namespace Rocket.AdditionalStructure
{
    public interface IAdditionalStructure:IRocket
    {
        double Weight { get; set; }
        bool IsOpened { get; set; }

        void MakeClosed()
        {
            IsOpened = false;
        }

        void MakeOpened()
        {
            IsOpened = true;
        }
    }
}
