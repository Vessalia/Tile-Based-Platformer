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
        public float aggroRange;

        public Enemy(Vector2 initialPos, Vector2 dim, AnimationManager animManager, float speed, float aggroRange) : base(initialPos, dim, animManager, speed)
        {
            state = new EnemyIdleState(this, animManager);
            zIdx = 1;
            facingLeft = true;
            this.aggroRange = aggroRange;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            GravitySystem.Update(this, dt);
        }

        public void CheckAggro(Vector2 playerPos)
        {
            if(state is EnemyIdleState)
            {
                ((EnemyIdleState)state).CheckAggro(playerPos);
            }
            else if(state is EnemyRunningState)
            {
                ((EnemyRunningState)state).CheckAggro(playerPos);
            }
        }

        public override void SetStunned() => SetState(new EnemyStunnedState(this, animManager));

        public override void SetDead() => SetState(new EnemyDeathState(this, animManager));

        public override bool IsAttackable()
        {
            return !(state is EnemyStunnedState);
        }
    }
}
