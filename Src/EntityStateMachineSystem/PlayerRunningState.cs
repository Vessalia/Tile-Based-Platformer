using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachineSystem
{
    public class PlayerRunningState : EntityState
    {
        private Player Player { get { return (Player)entity; } }

        public PlayerRunningState(Player entity, AnimationManager manager) : base(entity, manager) 
        {
            manager.LoadContent("run");
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
            float spd = 1;
            if(Player.IsFacingLeft())
            {
                spd = -1;
            }

            Player.vel.X = spd;
            Player.vel.Y = 0;

            Player.pos += Player.vel * dt;
            CollisionResolver.Resolve(Player, true);

            manager.Update(dt, "run");
        }

        private void HandleInput()
        {
            if((Player.IsInputUp("left") && Player.IsInputUp("right")) || (Player.IsInputDown("left") && Player.IsInputDown("right")))
            {
                Player.SetState(new PlayerIdleState(Player, manager));
            }
        }
    }
}
