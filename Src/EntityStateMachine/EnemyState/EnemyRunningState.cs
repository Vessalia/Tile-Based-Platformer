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
    internal class EnemyRunningState : EntityState
    {
        private Enemy Enemy { get { return (Enemy)entity; } }

        public EnemyRunningState(Entity entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("run");

            attacks.Add(new AttackBox(new RectangleF(entity.pos, entity.dim), entity, 0.5f, 8, Vector2.Zero));
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            float speed = Enemy.speed;
            if (Enemy.IsFacingLeft()) speed *= -1;

            Enemy.vel.X = speed;
            Enemy.vel.Y = 0;

            float epsilon = 0.001f;
            Enemy.pos.Y += epsilon;
            if (CollisionResolver.Resolve(entity, false) == null)
            {
                Enemy.SetState(new EnemyFreeFallState(Enemy, manager));
                Enemy.pos.Y -= epsilon;
            }

            manager.Update(dt, "run");
        }

        public void CheckAggro(Vector2 playerPos)
        {
            float threshold = Enemy.aggroRange;
            float tol = 0.1f;
            Vector2 distVect = Enemy.pos - playerPos;
            if (distVect.LengthSquared() < threshold && distVect.X > tol)
            {
                Enemy.SetDir(-1);
            }
            else if (distVect.LengthSquared() < threshold && distVect.X < -tol)
            {
                Enemy.SetDir(1);
            }
            else
            {
                attacks.Clear();
                Enemy.SetState(new EnemyIdleState(Enemy, manager));
            }
        }
    }
}
