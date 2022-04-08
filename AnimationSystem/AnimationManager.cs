using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.Src;

namespace TileBasedPlatformer.AnimationSystem
{
    public class AnimationManager
    {
        private AnimatedSprite sprite;
        private SpriteSheet spriteSheet;

        public AnimationManager(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            sprite = new AnimatedSprite(spriteSheet);
        }

        public void LoadContent(string animationName)
        {
            var transferSprite = new AnimatedSprite(spriteSheet);
            transferSprite.Play(animationName);
            sprite = transferSprite;
        }

        public void Update(float dt, string animationName)
        {
            sprite.Play(animationName);
            sprite.Update(dt);
        }

        public AnimatedSprite getCurrentSprite()
        {
            return sprite;
        }
    }
}
