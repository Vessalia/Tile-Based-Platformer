using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
{
    internal class PlayerLandingState : EntityState
    {
        private Player Player { get { return (Player)entity; } }
        private float timer = 0;

        public PlayerLandingState(Player entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("land");
            Player.vel.X = 0;
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            timer += dt;

            manager.Update(dt, "land");

            if (timer > manager.GetAnimationDuration())
            {
                Player.vel.Y = 0;
                Player.SetState(new PlayerIdleState(Player, manager));
            }
        }
    }
}
