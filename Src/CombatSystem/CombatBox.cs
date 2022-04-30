using MonoGame.Extended;

namespace TileBasedPlatformer.Src.CombatSystem
{
    public class CombatBox
    {
        public RectangleF box;
        public Entity owner;

        public CombatBox(RectangleF box, Entity owner)
        {
            this.box = box;
            this.owner = owner;
        }
    }
}
