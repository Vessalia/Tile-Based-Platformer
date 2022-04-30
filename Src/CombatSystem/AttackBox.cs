using MonoGame.Extended;

namespace TileBasedPlatformer.Src.CombatSystem
{
    public class AttackBox : CombatBox
    {
        public float damage;
        public float knockback;

        public AttackBox(RectangleF box, Entity owner, float damage, float knockback) : base(box, owner)
        {
            this.damage = damage;
            this.knockback = knockback;
        }
    }
}
