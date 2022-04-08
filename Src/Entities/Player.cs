using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace TileBasedPlatformer.Src.Entities
{
    public class Player : Entity
    {
        protected Vector2 vel;
        protected float speed;

        public Player(Vector2 initialPos, Vector2 dim) : base (initialPos, dim)
        {
            speed = 10;
        }

        public void SetXDir(int x)
        {
            vel.X = x;
            if (vel.LengthSquared() != 0)
            {
                vel.Normalize();
            }
            vel.X *= speed;
        }
    }
}
