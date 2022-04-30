using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachine.EnemyState;
using TileBasedPlatformer.Src.PhysicsSystems;

namespace TileBasedPlatformer.Src.Entities
{
    internal class Enemy : Entity
    {
        public Enemy(Vector2 initialPos, Vector2 dim, AnimationManager animManager, float speed) : base(initialPos, dim, animManager, speed)
        {
            state = new EnemyIdleState(this, animManager);
            zIdx = 1;
            facingLeft = true;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            GravitySystem.Update(this, dt);
        }

        public override void SetStunned() => SetState(new EnemyStunnedState(this, animManager));
        public override void SetDead() => SetState(new EnemyDeathState(this, animManager));
    }
}
