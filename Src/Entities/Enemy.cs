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

        protected Vector2 vel;
        protected float speed;

        public Enemy(Vector2 initialPos, Vector2 dim, AnimationManager animManager) : base(initialPos, dim, animManager)
        {
            this.colour = colour;
            speed = 10;
        }
    }
}
