using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachineSystem.PlayerState
{
    class PlayerJumpState : EntityState
    {
        private Player Player { get { return (Player)entity; } }

        public PlayerJumpState(Player entity, AnimationManager manager) : base(entity, manager) 
        {
            manager.LoadContent("jump");
            Player.vel.Y = -Player.speed;
        }

        public override void Update(float dt)
        {
            HandleInput();
            manager.Update(dt, "jump");
            if(Player.vel.Y <= 1)
            {
                Player.SetState(new PlayerIdleState(Player, manager));
            }

            CollisionResolver.Resolve(Player, true);
        }

        private void HandleInput()
        {
            int dir = Player.GetXDir();
            Player.SetDir(dir);
        }
    }
}
