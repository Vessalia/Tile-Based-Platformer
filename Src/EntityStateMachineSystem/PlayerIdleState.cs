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
        }

        public override void Draw(SpriteBatch sb)
        {
            var sprite = manager.getCurrentSprite();
            float scale = 2f / sprite.TextureRegion.Height;
            sb.Draw(sprite, Player.Pos + new Vector2(1, 0) / 2, 0, new Vector2(scale, scale));
        }

        public override void Update(float dt)
        {
            Player.vel = Vector2.Zero;
            manager.Update(dt, "idle");
        }
    }
}
