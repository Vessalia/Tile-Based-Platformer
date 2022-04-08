using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachineSystem;

namespace TileBasedPlatformer.Src
{
    public abstract class Entity
    {
        protected EntityState state;

        protected AnimationManager animManager;

        public Vector2 Pos { get; protected set; }
        protected Vector2 dim;

        public Entity(Vector2 initialPos, Vector2 dim, AnimationManager animManager)
        {
            Pos = initialPos;
            this.dim = dim;
            this.animManager = animManager;
        }

        public Entity(float initialX, float initialY)
        {
            Pos = new Vector2(initialX, initialY);
        }

        public void Update(float dt)
        {
            state.Update(dt);
        }

        public void Draw(SpriteBatch sb)
        {
            state.Draw(sb);
        }

        public void SetState(EntityState state)
        {
            this.state = state;
        }
    }
}
