using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachineSystem;

namespace TileBasedPlatformer.Src.Entities
{
    public class Player : Entity
    {
        public Vector2 vel;
        private float speed;

        public Player(Vector2 initialPos, Vector2 dim, AnimationManager animManager) : base (initialPos, dim, animManager)
        {
            state = new PlayerIdleState(this, animManager);
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
