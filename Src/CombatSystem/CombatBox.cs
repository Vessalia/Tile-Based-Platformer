using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace TileBasedPlatformer.Src.CombatSystem
{
    public class CombatBox
    {
        public RectangleF box;
        public Entity owner;
        public Vector2 offset;

        public CombatBox(RectangleF box, Entity owner, Vector2 offset)
        {
            this.box = box;
            this.owner = owner;
            this.offset = offset;
        }
    }
}
