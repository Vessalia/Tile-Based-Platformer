using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachine.EnemyState;

namespace TileBasedPlatformer.Src.Entities
{
    internal class Enemy : Entity
    {
        public Enemy(Vector2 initialPos, Vector2 dim, AnimationManager animManager, float speed) : base(initialPos, dim, animManager, speed)
        {
            state = new EnemyIdleState(this, animManager);
            zIdx = 1;
        }
    }
}
