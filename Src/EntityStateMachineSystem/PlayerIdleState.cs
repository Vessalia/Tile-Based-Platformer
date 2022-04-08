using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachineSystem
{
    class PlayerIdleState : EntityState
    {
        public PlayerIdleState(Player player, AnimationManager manager) : base(player, manager) 
        {
            manager.LoadContent("idle");
        }

        public override void Draw(SpriteBatch sb)
        {
            var sprite = manager.getCurrentSprite();
            float scale = 2f / sprite.TextureRegion.Height;
            sb.Draw(sprite, pos + new Vector2(1, 0) / 2, 0, new Vector2(scale, scale));
        }

        public override void Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
