using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachineSystem
{
    class PlayerIdleState : EntityState
    {
        private Player Player { get { return (Player)entity; } }

        public PlayerIdleState(Player entity, AnimationManager manager) : base(entity, manager) 
        {
            manager.LoadContent("idle");
            Player.vel = Vector2.Zero;
        }

        public override void Draw(SpriteBatch sb)
        {
            var sprite = manager.getCurrentSprite();
            float scale = 2f / sprite.TextureRegion.Height;
            if (Player.IsFacingLeft())
            {
                sprite.Effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                sprite.Effect = SpriteEffects.None;
            }
            sb.Draw(sprite, Player.pos + new Vector2(1, 0) / 2, 0, new Vector2(scale, scale));
        }

        public override void Update(float dt)
        {
            HandleInput();
            manager.Update(dt, "idle");
        }

        private void HandleInput()
        {
            int dir = 0;
            if (Player.IsInputDown("right"))
            {
                dir += 1;
            }
            if(Player.IsInputDown("left"))
            {
                dir -= 1;
            }

            if (dir == -1)
            {
                Player.SetFacingLeft(true);
            }
            else if (dir == 1)
            {
                Player.SetFacingLeft(false);
            }

            if(dir != 0)
            {
                Player.SetState(new PlayerRunningState(Player, manager));
            }
        }
    }
}
