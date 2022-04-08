namespace TileBasedPlatformer.Src
{
    public abstract class Controller
    {
        protected readonly Entity owner;

        public Controller(Entity owner)
        {
            this.owner = owner;
        }

        public abstract void HandleInput();
    }
}
