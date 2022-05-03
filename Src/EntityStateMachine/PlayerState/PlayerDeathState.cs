using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
{
    internal class PlayerDeathState : EntityState
    {
        private Player Player { get { return (Player)entity; } }
        private float timer = 0;

        public PlayerDeathState(Entity entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("death");
            Player.zIdx = 10;
            Player.vel.X = 0;

            attacks.Clear();
            bodies.Clear();
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            timer += dt;
            if (manager.GetAnimationDuration() <= timer)
            {
                Player.isDead = true;
                return;
            }
            manager.Update(dt, "death");
        }
    }
}
