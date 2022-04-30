using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.Src.Core;

namespace TileBasedPlatformer.Src.CombatSystem
{
    public interface ICombat
    {
        public void DealDamage(ICombat combatable)
        {
            AttackBox attack = CollisionResolver.Attack(this, combatable);
            if (attack != null)
            {
                combatable.TakeDamage(attack);
            }
        }

        public void TakeDamage(AttackBox attack);
        public List<AttackBox> GetAttackBoxes();
        public List<BodyBox> GetBodyBoxes();

        public bool IsAttackable();
    }
}
