using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.Src;
using TileBasedPlatformer.Src.CombatSystem;

namespace TileBasedPlatformer.AnimationSystem
{
    public class AnimationManager
    {
        private AnimatedSprite sprite;
        private SpriteSheet spriteSheet;

        private string animName;

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
            animName = animationName;
        }

        public void Update(float dt, string animationName)
        {
            sprite.Play(animationName);
            animName = animationName;
            sprite.Update(dt);
        }

        public AnimatedSprite GetCurrentSprite()
        {
            return sprite;
        }

        public SpriteSheet GetSpriteSheet()
        {
            return spriteSheet;
        }

        public float GetAnimationDuration()
        {
            spriteSheet.Cycles.TryGetValue(animName, out var cycle);
            int count = cycle.Frames.Count;
            return cycle.FrameDuration * count;
        }
    }
}
