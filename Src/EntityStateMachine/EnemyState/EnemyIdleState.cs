using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.EnemyState
{
    internal class EnemyIdleState : EntityState
    {
        private Enemy Enemy { get { return (Enemy)entity; } }

        public EnemyIdleState(Enemy entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("idle");
            Enemy.vel = Vector2.Zero;
        }
        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            HandleInput();
            manager.Update(dt, "idle");
        }

        private void HandleInput()
        {

        }
    }
}
