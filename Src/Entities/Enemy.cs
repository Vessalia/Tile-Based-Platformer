using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.Entities
{
    class Enemy : Entity
    {
        private Color colour;

        protected Vector2 vel;
        protected float speed;

        public Enemy(Vector2 initialPos, Vector2 dim, Color colour) : base(initialPos, dim)
        {
            this.colour = colour;
            speed = 10;
        }
    }
}
