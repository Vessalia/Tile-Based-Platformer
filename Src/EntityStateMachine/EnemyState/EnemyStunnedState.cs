using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.EnemyState
{
    internal class EnemyStunnedState : EntityState
    {
        private Enemy Enemy { get { return (Enemy)entity; } }
        private float timer = 0;

        public EnemyStunnedState(Entity entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("hit");
            Enemy.zIdx = 0;
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            timer += dt;
            if (manager.GetAnimationDuration() <= timer)
            {
                Enemy.SetState(new EnemyIdleState(Enemy, manager));
                return;
            }
            manager.Update(dt, "hit");
        }
    }
}
