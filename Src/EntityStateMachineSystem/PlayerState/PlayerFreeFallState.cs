using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachineSystem.PlayerState
{
    internal class PlayerFreeFallState : EntityState
    {
        private Player Player { get { return (Player)entity; } }
        private string anim;
        public PlayerFreeFallState(Player entity, AnimationManager manager) : base(entity, manager)
        {
            if (Player.vel.Y > 0) anim = "rising";
            else anim = "falling";
            manager.LoadContent(anim);
        }

        public override void Update(float dt)
        {
            HandleInput();

            if (Player.vel.Y >= -0.2f && Player.vel.Y <= 0.2f && !anim.Equals("jump_peak"))
            {
                anim = "jump_peak";
                manager.LoadContent(anim);
            }
            else if (Player.vel.Y < 0.2f && !anim.Equals("falling"))
            {
                anim = "falling";
                manager.LoadContent(anim);
            }

            float epsilon = 0.001f;
            Player.pos.Y += epsilon;
            if (CollisionResolver.Resolve(entity, false) == CollisionSide.Top)
            {
                Player.SetState(new PlayerIdleState(Player, manager));
            }
            Player.pos.Y -= epsilon;

            manager.Update(dt, anim);
        }

        private void HandleInput()
        {
            int dir = Player.GetXDir();
            Player.SetDir(dir);
        }
    }
}
