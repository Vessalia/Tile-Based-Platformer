using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
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
        public Vector2 dim;

        public float speed;
        public float terminalVel;

        protected float scale;

        public virtual Vector2 Pos => pos;

        public Entity(Vector2 initialPos, Vector2 dim, AnimationManager animManager, float speed, float scale = 1)
        {
            pos = initialPos;
            this.dim = dim;
            this.animManager = animManager;

            this.speed = speed;
            terminalVel = 2 * speed;

            this.scale = dim.X * scale;
        }

        public void Draw(SpriteBatch sb)
        {
            var sprite = animManager.GetCurrentSprite();
            float scale = this.scale / sprite.TextureRegion.Width;
            if (facingLeft)
            {
                sprite.Effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                sprite.Effect = SpriteEffects.None;
            }

            Texture2D texture = sprite.TextureRegion.Texture;
            Rectangle bounds = sprite.TextureRegion.Bounds;
            sb.Draw(texture, pos + new Vector2(1 * dim.X, 2 * dim.Y) / 2, bounds, sprite.Color * sprite.Alpha, 0, sprite.Origin + new Vector2(0, sprite.TextureRegion.Height) / 2, scale, sprite.Effect, sprite.Depth);
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
