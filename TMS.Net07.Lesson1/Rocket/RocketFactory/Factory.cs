namespace Rocket.RocketFactory
{
    public abstract class Factory
    {
        public abstract IRocket Create(int n);
    }
}