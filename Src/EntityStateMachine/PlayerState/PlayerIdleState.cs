using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
{
    class PlayerIdleState : EntityState
    {
        private Player Player { get { return (Player)entity; } }

        public PlayerIdleState(Player entity, AnimationManager manager) : base(entity, manager) 
        {
            manager.LoadContent("idle");
            Player.vel = Vector2.Zero;
        }

        public override void Update(float dt)
        {
            HandleInput();
            manager.Update(dt, "idle");
        }

        private void HandleInput()
        {
            int dir = Player.GetXDir();
            Player.SetDir(dir);

            if (Player.IsInputDown("up"))
            {
                Player.SetState(new PlayerJumpState(Player, manager));
            }
            else if (dir != 0)
            {
                Player.SetState(new PlayerRunningState(Player, manager));
            }
        }
    }
}
