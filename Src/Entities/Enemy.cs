using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;

namespace TileBasedPlatformer.Src.Entities
{
    class Enemy : Entity
    {
        private Color colour;
        protected float speed;

        public Enemy(Vector2 initialPos, Vector2 dim, AnimationManager animManager, float speed) : base(initialPos, dim, animManager, speed) { }
    }
}
