namespace Rocket.AdditionalStructure
{
    public interface IAdditionalStructure
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
