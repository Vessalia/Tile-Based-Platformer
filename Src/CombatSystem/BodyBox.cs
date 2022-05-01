using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace TileBasedPlatformer.Src.CombatSystem
{
    public class BodyBox : CombatBox
    {
        public BodyBox(RectangleF box, Entity owner, Vector2 offset) : base(box, owner, offset)
        {

        }
    }
}
