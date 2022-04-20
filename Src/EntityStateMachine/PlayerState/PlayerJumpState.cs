using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
{
    class PlayerJumpState : EntityState
    {
        private Player Player { get { return (Player)entity; } }
        private float timer = 0;

        public PlayerJumpState(Player entity, AnimationManager manager) : base(entity, manager) 
        {
            manager.LoadContent("jump");
            Player.vel.X = 0;
        }

        public override void Update(float dt)
        {
            HandleInput();

            timer += dt;

            manager.Update(dt, "jump");

            if (timer > manager.GetAnimationDuration())
            {
                Player.vel.Y = Player.speed;
                Player.SetState(new PlayerFreeFallState(Player, manager));
            }
        }

        private void HandleInput()
        {
            int dir = Player.GetXDir();
            Player.SetDir(dir);
        }
    }
}
