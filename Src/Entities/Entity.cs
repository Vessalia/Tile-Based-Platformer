using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.CameraSystem;
using TileBasedPlatformer.Src.EntityStateMachine;

namespace TileBasedPlatformer.Src
{
    public abstract class Entity : ICameraTarget
    {
        protected bool facingLeft = false; 
        protected EntityState state;

        protected AnimationManager animManager;

        public Vector2 pos;
        public Vector2 vel;
        public Location dim;

        public float speed;
        public float terminalVel;

        private float scale;

        private float drawXOffset;
        private float drawYOffset;

        public virtual Vector2 Pos => pos;

        public Entity(Vector2 initialPos, Location dim, AnimationManager animManager, float speed, float scale, float drawXOffset = 0, float drawYOffset = 0)
        {
            pos = initialPos;
            this.dim = dim;
            this.animManager = animManager;

            this.speed = speed;
            terminalVel = 2 * speed;

            this.scale = scale;

            this.drawXOffset = drawXOffset;
            this.drawYOffset = drawYOffset;
        }

        public void Draw(SpriteBatch sb)
        {
            var sprite = animManager.GetCurrentSprite();
            float scale = this.scale / sprite.TextureRegion.Height;
            if (facingLeft)
            {
                sprite.Effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                sprite.Effect = SpriteEffects.None;
            }
            sb.Draw(sprite, pos + new Vector2(dim.x + drawXOffset, drawYOffset) / 2, 0, new Vector2(scale, scale));
        }

        public Entity(float initialX, float initialY)
        {
            pos = new Vector2(initialX, initialY);
        }

        public virtual void Update(float dt)
        {
            state.Update(dt);

            int maxSteps = 10;
            for (int step = 0; step < maxSteps; step++)
            {
                pos.X += vel.X * dt / maxSteps;
                CollisionResolver.Resolve(this, true);
            }
            for(int step = 0; step < maxSteps; step++)
            {
                pos.Y -= vel.Y * dt / maxSteps;
                CollisionResolver.Resolve(this, false);
            }
        }

        public void SetState(EntityState state)
        {
            this.state = state;
        }

        public void SetFacingLeft(bool facingLeft)
        {
            this.facingLeft = facingLeft;
        }

        public bool IsFacingLeft()
        {
            return facingLeft;
        }

        public void SetDir(int dir)
        {
            if (dir == -1)
            {
                facingLeft = true;
            }
            else if (dir == 1)
            {
                facingLeft = false;
            }
        }
    }
}
