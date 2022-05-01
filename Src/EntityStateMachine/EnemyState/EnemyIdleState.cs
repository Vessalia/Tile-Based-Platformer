using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.CombatSystem;
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

            attacks.Add(new AttackBox(new RectangleF(entity.pos, entity.dim), entity, 0.000001f, 8, Vector2.Zero));
        }
        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            HandleInput();
            Enemy.vel.X = 0;
            manager.Update(dt, "idle");
        }

        private void HandleInput()
        {

        }
    }
}
