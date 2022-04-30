using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.EnemyState
{
    internal class EnemyDeathState : EntityState
    {
        private Enemy Enemy { get { return (Enemy)entity; } }
        private float timer = 0;

        public EnemyDeathState(Entity entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("death");
            attacks.Clear();
            Enemy.zIdx = -1;
            Enemy.vel = Vector2.Zero;

            attacks.Clear();
            bodies.Clear();
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);

            timer += dt;

            manager.Update(dt, "death");

            if (manager.GetAnimationDuration() < timer)
            {
                Enemy.isDead = true;
                return;
            }
        }
    }
}
