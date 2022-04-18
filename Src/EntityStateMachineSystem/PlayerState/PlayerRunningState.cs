using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachineSystem.PlayerState
{
    public class PlayerRunningState : EntityState
    {
        private Player Player { get { return (Player)entity; } }

        public PlayerRunningState(Player entity, AnimationManager manager) : base(entity, manager) 
        {
            manager.LoadContent("run");
            Player.vel.Y = 0;
        }

        public override void Update(float dt)
        {
            HandleInput();
            float speed = Player.speed;
            if (Player.IsFacingLeft()) speed *= -1;

            Player.vel.X = speed;
            Player.vel.Y = 0;

            manager.Update(dt, "run");

            if (Player.IsInputDown("up"))
            {
                Player.SetState(new PlayerJumpState(Player, manager));
            }
            else if ((Player.IsInputUp("left") && Player.IsInputUp("right")) || (Player.IsInputDown("left") && Player.IsInputDown("right")))
            {
                Player.SetState(new PlayerIdleState(Player, manager));
            }

            float epsilon = 0.001f;
            Player.pos.Y += epsilon;
            if (CollisionResolver.Resolve(entity, false) == null)
            {
                Player.SetState(new PlayerFreeFallState(Player, manager));
                Player.pos.Y -= epsilon;
            }
        }

        private void HandleInput()
        {
            int dir = Player.GetXDir();
            Player.SetDir(dir);
        }
    }
}
