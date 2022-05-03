using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.CombatSystem;
using TileBasedPlatformer.Src.Core;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.EnemyState
{
    internal class EnemyFreeFallState : EntityState
    {
        private Enemy Enemy { get { return (Enemy)entity; } }

        public EnemyFreeFallState(Enemy entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("idle");
            Enemy.vel = Vector2.Zero;

            attacks.Add(new AttackBox(new RectangleF(entity.pos, entity.dim), entity, 0.5f, 8, Vector2.Zero));
        }
        public override void Update(float dt, Vector2 pos)
        {
            Enemy.vel.X = 0;
            float epsilon = 0.001f;
            Enemy.pos.Y += epsilon;
            if (CollisionResolver.Resolve(entity, false) == CollisionSide.Top)
            {
                attacks.Clear();

                Enemy.SetState(new EnemyIdleState(Enemy, manager));
                return;
            }
            else Enemy.pos.Y -= epsilon;
        }
    }
}
